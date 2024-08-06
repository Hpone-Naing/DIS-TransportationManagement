using TransportationManagement.Classes;
using TransportationManagement.Factories;
using TransportationManagement.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using TransportationManagement.Models;
using ClosedXML.Excel;
using System.Text.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;

namespace TransportationManagement.Controllers.VechicleData
{
    public class VehicleDataController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public VehicleDataController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private void AddSearchDatasToViewBag(string searchString, string searchOption, AdvanceSearch advanceSearch)
        {
            ViewBag.SearchString = searchString;
            ViewBag.SearchOption = searchOption;
            ViewBag.CctvInstalled = advanceSearch.CctvInstalled;
            ViewBag.POSInstalled = advanceSearch.POSInstalled;
            ViewBag.TelematicDeviceInstalled = advanceSearch.TelematicDeviceInstalled;
            ViewBag.YBSCompany = advanceSearch.YBSCompany;
            ViewBag.Manufacturer = advanceSearch.Manufacturer;
            ViewBag.YBSName = advanceSearch.YBSName;
            ViewBag.FuelType = advanceSearch.FuelType;
        }

        private string MakeExcelFileName(string searchString, bool ExportAll, int? pageNo)
        {
            if (ExportAll)
            {
                return "VehicleDataမှတ်တမ်းအားလုံး" + DateTime.Now + ".xlsx";
            }
            else
            {
                if (searchString != null && !string.IsNullOrEmpty(searchString))
                    return "VehicleDataမှတ်တမ်းရှာဖွေမှု(" + searchString + ")" + DateTime.Now + ".xlsx";
                else
                    return "VehicleDataမှတ်တမ်း PageNumber( " + pageNo + " )" + DateTime.Now + ".xlsx";
            }

        }
        public IActionResult List(int? pageNo)
        {
            string searchString = Request.Query["SearchString"];
            string searchOption = Request.Query["searchOption"];
            AdvanceSearch advanceSearch = Utility.MakeAdvanceSearch(HttpContext);
            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            AddSearchDatasToViewBag(searchString, searchOption, advanceSearch);
            try
            {
                if (!SessionUtil.IsActiveSession(HttpContext))
                    return RedirectToAction("Index", "Login");

                
                //PagingList<VehicleData> vehicleDatas1 = _serviceFactory.CreateVehicleDataService().GetAllVehiclesWithPagin(searchString, advanceSearch, pageNo??1, pageSize);
                if (Request.Query["export"] == "excel")
                {
                    Console.WriteLine("here export excel..........");
                    bool ExportAll = Request.Query["ExportAll"] == "true";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Vehicle Data Report");
                        var headerCell = ws.Cell(1, 1);
                        headerCell.Value = "ရန်ကုန်တိုင်းဒေသကြီး ပုဂ္ဂလိကသယ်ယူပို့ဆောင်ရေးကြီးကြပ်မှုကော်မတီ (YRTC)";
                        headerCell.AsRange().AddToNamed("HeaderRange");
                        ws.Range(1, 1, 1, 13).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(1, 1, 1, 13).Style.Font.Bold = true;
                        ws.Range(1, 1, 1, 13).Style.Font.FontSize = 16;

                        var secondHeaderCell = ws.Cell(2, 1);
                        secondHeaderCell.Value = "YBS ကုမ္ပဏီ၊ ယာဉ်လိုင်းများအလိုက် ယာဉ်စာရင်း";
                        secondHeaderCell.AsRange().AddToNamed("HeaderRange");
                        ws.Range(2, 1, 2, 13).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(2, 1, 2, 13).Style.Font.Bold = true;
                        ws.Range(2, 1, 2, 13).Style.Font.FontSize = 15;

                        ws.Cell(3, 12).Value = "ITS System";
                        ws.Range(3, 12, 3, 14).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range(3, 12, 3, 14).Style.Font.Bold = true;
                        ws.Range(3, 12, 3, 14).Style.Font.FontSize = 14;
                        ws.Range("A3:N3").Style.Fill.BackgroundColor = XLColor.DodgerBlue;

                        DataTable dt = _serviceFactory.CreateVehicleDataService().MakeVehicleDataExcelData(_serviceFactory.CreateVehicleDataService().GetAllVehiclesWithPaginForExcelExport(searchString, advanceSearch, pageNo ?? 1, pageSize), ExportAll);
                        ws.Cell(4, 1).InsertTable(dt);
                        ws.Range("A4:N4").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                        for (int row = 5; row < dt.Rows.Count + 5; row++)
                        {
                            // Telematic Device Installed
                            var cellTelematic = ws.Cell(row, 12);
                            if (cellTelematic.GetString() == "ü" || cellTelematic.GetString() == "û")
                            {
                                cellTelematic.Style.Font.FontName = "Wingdings";
                            }
                            else
                            {
                                cellTelematic.Style.Font.FontName = "Times New Roman";
                            }

                            // CCTV Installed
                            var cellCCTV = ws.Cell(row, 13);
                            if (cellCCTV.GetString() == "ü" || cellCCTV.GetString() == "û")
                            {
                                cellCCTV.Style.Font.FontName = "Wingdings";
                            }
                            else
                            {
                                cellCCTV.Style.Font.FontName = "Times New Roman";
                            }

                            // POS Installed
                            var cellPOS = ws.Cell(row, 14);
                            if (cellPOS.GetString() == "ü" || cellPOS.GetString() == "û")
                            {
                                cellPOS.Style.Font.FontName = "Wingdings";
                            }
                            else
                            {
                                cellPOS.Style.Font.FontName = "Times New Roman";
                            }
                        }
                        ws.Rows().AdjustToContents();
                        ws.Rows().Height = 20;
                        ws.Columns().AdjustToContents();
                        ws.Columns().Style.Font.FontSize = 14;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", MakeExcelFileName(searchString, ExportAll, pageNo));
                        }
                    }
                }
                return View(_serviceFactory.CreateVehicleDataService().GetAllVehiclesWithPagin(searchString, advanceSearch, pageNo ?? 1, pageSize, searchOption));
            } catch(Exception ne)
            {
                PagingList<VehicleData> vehicleDatas = _serviceFactory.CreateVehicleDataService().GetAllVehiclesWithPagin(searchString, advanceSearch, pageNo ?? 1, pageSize, searchOption);
                Console.WriteLine("Error: " + ne);
                Utility.AlertMessage(this, "Data Issue. Please fill VehicleData in database", "alert-danger");
                return View(vehicleDatas);
            }
            
        }

        private List<SelectListItem> GetItemsFromList<T>(List<T> list, string valuePropertyName, string textPropertyName)
        {
            var lstItems = new List<SelectListItem>();

            foreach (T item in list)
            {
                var itemType = item.GetType();
                var valueProperty = itemType.GetProperty(valuePropertyName);
                var textProperty = itemType.GetProperty(textPropertyName);

                if (valueProperty != null && textProperty != null)
                {
                    var value = valueProperty.GetValue(item)?.ToString();
                    var text = textProperty.GetValue(item)?.ToString();

                    lstItems.Add(new SelectListItem
                    {
                        Value = value,
                        Text = text
                    });
                }
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "ရွေးချယ်ရန်"
            };

            lstItems.Insert(0, defItem);
            return lstItems;
        }
        private void AddViewBag()
        {
            List<FuelType> uniqueFuelTypes = _serviceFactory.CreateFuelTypeService().GetUniqueFuelTypes();
            List<Manufacturer> uniqueManufacturers = _serviceFactory.CreateManufacturerService().GetUniqueManufacturers();
            //List<YBSCompany> uniqueYBSCompanies = _serviceFactory.CreateYBSCompanyService().GetUniqueYBSCompanys();
            //List<YBSType> uniqueYBSTypes = _serviceFactory.CreateYBSTypeService().GetUniqueYBSTypes();

            ViewBag.FuelTypes = GetItemsFromList(uniqueFuelTypes, "FuelTypePkid", "FuelTypeName");
            ViewBag.Manufacturers = GetItemsFromList(uniqueManufacturers, "ManufacturerPkid", "ManufacturerName");
            ViewBag.YBSCompanies = _serviceFactory.CreateYBSCompanyService().GetSelectListYBSCompanys();//GetItemsFromList(uniqueYBSCompanies, "YBSCompanyPkid", "YBSCompanyName");
            ViewBag.YBSTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId();//GetItemsFromList(uniqueYBSTypes, "YBSTypePkid", "YBSTypeName");
        }
        public IActionResult Create()
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            AddViewBag();
            return View();
        }

       
        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(VehicleData vehicleData)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateVehicleDataService().CreateVehicle(vehicleData))
            {
                Utility.AlertMessage(this, "Save Success", "alert-success");
                try
                {
                    return RedirectToAction(nameof(List));
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill VehicleData in database", "alert-danger");
                    return View();
                }
                catch (SqlException se)
                {
                    Utility.AlertMessage(this, "Internal Server Error", "alert-danger");
                    return View();
                }
            }
            else
            {
                Utility.AlertMessage(this, "Save Fail.Internal Server Error", "alert-danger");
                AddViewBag();
                return View();
            }
        }

        public IActionResult Edit(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            AddViewBag();
            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataById(Id);
            ViewBag.YBSTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(vehicleData.YBSCompany.YBSCompanyPkid);
            return View(vehicleData);
        }

        public IActionResult Details(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByIdEgerLoad(Id);
            return View(vehicleData);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(VehicleData vehicleData)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateVehicleDataService().EditVehicle(vehicleData))
            {

                Utility.AlertMessage(this, "Edit Success", "alert-success");
                return RedirectToAction(nameof(List));
            }
            else
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                AddViewBag();
                ViewBag.YBSTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(vehicleData.YBSCompany.YBSCompanyPkid);
                return View();
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataById(Id);
            if (_serviceFactory.CreateVehicleDataService().DeleteVehicle(vehicleData))
            {
                Utility.AlertMessage(this, "Delete Success", "alert-success");
                return RedirectToAction(nameof(List));
            }
            else
            {
                Utility.AlertMessage(this, "Delete Fail.Internal Server Error", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        public List<VehicleData> ParseExcelFile(Stream stream)
        {
            var vehicleDatas = new List<VehicleData>();
            //Create a workbook instance
            //Opens an existing workbook from a stream.
            using (var workbook = new XLWorkbook(stream))
            {
                //Lets assume the First Worksheet contains the data
                var worksheet = workbook.Worksheet("Vehicle List");
                var rows = worksheet.RangeUsed().RowsUsed().Skip(4);
                foreach (var row in rows)
                {
                    if (row == null || row.Cell(4).GetValue<string>() == null)
                    {
                        continue;
                    }
                    var vehicleData = new VehicleData();
                    vehicleData.SerialNo = row.Cell(1).GetValue<string>() ?? row.Cell(1).GetValue<string>();
                    string companyName = row.Cell(2).GetValue<string>() ?? row.Cell(2).GetValue<string>();
                    vehicleData.VehicleNumber = row.Cell(3).GetValue<string>() ?? row.Cell(3).GetValue<string>();
                    string ybsTypeName = row.Cell(4).GetValue<string>() ?? row.Cell(4).GetValue<string>();
                    string manufacturerName = row.Cell(5).GetValue<string>() ?? row.Cell(6).GetValue<string>();
                    vehicleData.YBSName = row.Cell(6).GetValue<string>() ?? row.Cell(5).GetValue<string>();
                    vehicleData.ManufacturedYear = row.Cell(8).GetValue<string>() ?? row.Cell(8).GetValue<string>();
                    string fuelTypeName = row.Cell(10).GetValue<string>() ?? row.Cell(10).GetValue<string>();
                    vehicleData.CngQty = row.Cell(12).GetValue<string>()  ?? row.Cell(12).GetValue<string>();
                    vehicleData.OperatorName = row.Cell(13).GetValue<string>()  ?? row.Cell(13).GetValue<string>();
                    vehicleData.RegistrantOperatorName = row.Cell(14).GetValue<string>()  ?? row.Cell(14).GetValue<string>();
                    //vehicleData.TelematicDeviceInstalled = (row.Cell(16).GetValue<string>() == "\u00FC" || row.Cell(16).GetValue<string>() == "yes") ? "yes" : "no";
                    string telematicDeviceInstall = row.Cell(16).GetValue<string>();
                    switch(telematicDeviceInstall)
                    {
                        case "\u00FC":
                            vehicleData.TelematicDeviceInstalled = "yes";
                            break;
                        case "\u00FB":
                            vehicleData.TelematicDeviceInstalled = "no";
                            break;
                        default:
                            vehicleData.TelematicDeviceInstalled = telematicDeviceInstall;
                            break;
                    }

                    //vehicleData.CctvInstalled = (row.Cell(17).GetValue<string>() == "\u00FC" || row.Cell(16).GetValue<string>() == "yes") ? "yes" : "no";
                    string cctvInstall = row.Cell(17).GetValue<string>();
                    switch (cctvInstall)
                    {
                        case "\u00FC":
                            vehicleData.CctvInstalled = "yes";
                            break;
                        case "\u00FB":
                            vehicleData.CctvInstalled = "no";
                            break;
                        default:
                            vehicleData.CctvInstalled = cctvInstall;
                            break;
                    }
                    //vehicleData.POSInstalled = (row.Cell(18).GetValue<string>() == "\u00FC" || row.Cell(16).GetValue<string>() == "yes") ? "yes" : "no";
                    string posInstall = row.Cell(18).GetValue<string>();
                    switch (posInstall)
                    {
                        case "\u00FC":
                            vehicleData.POSInstalled = "yes";
                            break;
                        case "\u00FB":
                            vehicleData.POSInstalled = "no";
                            break;
                        default:
                            vehicleData.POSInstalled = posInstall;
                            break;
                    }
                    YBSCompany ybsCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyByName(companyName);
                    if(ybsCompany == null)
                    {
                        if(companyName != null)
                        {
                            YBSCompany newYBSCompany = new YBSCompany()
                            {
                                YBSCompanyName = companyName
                            };
                            _serviceFactory.CreateYBSCompanyService().CreateYBSCompany(newYBSCompany);
                            ybsCompany = newYBSCompany;
                            /*YBSType newYBSType = new YBSType()
                            {
                                YBSTypeName = ybsTypeName,
                                YBSCompany = newYBSCompany
                            };
                            _serviceFactory.CreateYBSTypeService().CreateYBSType(newYBSType);
                            ybsType = newYBSType;*/
                        }
                    }
                    YBSType ybsType = _serviceFactory.CreateYBSTypeService().FindYBSTypeByNameAndCompany(ybsTypeName, ybsCompany.YBSCompanyPkid);
                    if (ybsType == null)
                    {
                        if(ybsTypeName != null)
                        {
                            YBSType newYBSType = new YBSType()
                            {
                                YBSTypeName = ybsTypeName, 
                                YBSCompany = ybsCompany
                            };
                            _serviceFactory.CreateYBSTypeService().CreateYBSType(newYBSType);
                            ybsType = newYBSType;
                        }
                    }
                    
                    FuelType fuelType = _serviceFactory.CreateFuelTypeService().FindFuelTypeByName(fuelTypeName);
                    if(fuelType == null)
                    {
                        if(fuelTypeName != null)
                        {
                            FuelType newFuelType = new FuelType()
                            {
                                FuelTypeName = fuelTypeName
                            };
                            _serviceFactory.CreateFuelTypeService().CreateFuelType(newFuelType);
                            fuelType = newFuelType;
                        }
                    }
                    Manufacturer manufacturer = _serviceFactory.CreateManufacturerService().FindManufacturerByName(manufacturerName);
                    if(manufacturer == null)
                    {
                        if(manufacturerName != null)
                        {
                            Manufacturer newManufacturer = new Manufacturer()
                            {
                                ManufacturerName = manufacturerName
                            };
                            _serviceFactory.CreateManufacturerService().CreateManufacturer(newManufacturer);
                            manufacturer = newManufacturer;
                        }
                    }
                    vehicleData.YBSCompany = ybsCompany;
                    vehicleData.YBSType = ybsType;
                    vehicleData.FuelType = fuelType;
                    vehicleData.Manufacturer = manufacturer;
                    if(ybsTypeName != null && companyName != null && fuelTypeName != null && manufacturerName != null)
                    {
                        VehicleData existingVehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByVehicleNumber(vehicleData.VehicleNumber);
                        if (existingVehicleData == null)
                        {
                            vehicleDatas.Add(vehicleData);
                        }
                        else
                        {
                            existingVehicleData.SerialNo = vehicleData.SerialNo;
                            existingVehicleData.YBSCompany = ybsCompany;
                            existingVehicleData.VehicleNumber = vehicleData.VehicleNumber;
                            existingVehicleData.YBSType = ybsType;
                            existingVehicleData.YBSName = vehicleData.YBSName;
                            existingVehicleData.Manufacturer = manufacturer;
                            existingVehicleData.ManufacturedYear = vehicleData.ManufacturedYear;
                            existingVehicleData.CngQty = vehicleData.CngQty;
                            existingVehicleData.OperatorName = vehicleData.OperatorName;
                            existingVehicleData.RegistrantOperatorName = vehicleData.RegistrantOperatorName;
                            existingVehicleData.TelematicDeviceInstalled = vehicleData.TelematicDeviceInstalled;
                            existingVehicleData.CctvInstalled = vehicleData.CctvInstalled;
                            existingVehicleData.POSInstalled = vehicleData.POSInstalled;
                            _serviceFactory.CreateVehicleDataService().EditVehicle(existingVehicleData);
                        }
                    }
                }           
            }
            return vehicleDatas;
        }

        [HttpPost]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            try
            {
                if (file == null || (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && file.ContentType != "application/vnd.ms-excel"))
                {
                    Utility.AlertMessage(this, "Invalid file format. Please upload an Excel file", "alert-danger");
                    return RedirectToAction(nameof(List));
                }
                if (file != null && file.Length > 0)
                {
                    

                    var vehicleDatas = ParseExcelFile(file.OpenReadStream());
                    foreach (VehicleData vehicleData in vehicleDatas)
                    {
                        _serviceFactory.CreateVehicleDataService().CreateVehicle(vehicleData);
                    }
                    return RedirectToAction("List");
                }
                Utility.AlertMessage(this, "Something went wrong", "alert-danger");
                return RedirectToAction(nameof(List));
            }
            catch(Exception e)
            {
                Utility.AlertMessage(this, "Worksheet name must be Vehicle List", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        public IActionResult DownloadExcelTemplate()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/template", "vehicleDataTemplate.xlsx");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = "vehicleDataTemplate.xlsx";

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        public JsonResult GetYBSTypeByYBSCompanyId(int ybsCompanyId)
        {
            List<SelectListItem> ybsTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(ybsCompanyId);
            return Json(ybsTypes);
        }

    }
}

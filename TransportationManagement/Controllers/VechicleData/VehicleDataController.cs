using TransportationManagement.Classes;
using TransportationManagement.Factories;
using TransportationManagement.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using TransportationManagement.Models;

namespace TransportationManagement.Controllers.VechicleData
{
    public class VehicleDataController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public VehicleDataController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private void AddSearchDatasToViewBag(string searchString, AdvanceSearch advanceSearch)
        {
            ViewBag.SearchString = searchString;
            ViewBag.CctvInstalled = advanceSearch.CctvInstalled;
            ViewBag.CngQty = advanceSearch.CngQty;
            ViewBag.TotalBusStop = advanceSearch.TotalBusStop;
            ViewBag.TotalBusStopOption = advanceSearch.TotalBusStopOption;
        }
        public IActionResult List(int? pageNo)
        {
            try
            {
                if (!SessionUtil.IsActiveSession(HttpContext))
                    return RedirectToAction("Index", "Login");

                string searchString = Request.Query["SearchString"];
                AdvanceSearch advanceSearch = Utility.MakeAdvanceSearch(HttpContext);

                int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
                AddSearchDatasToViewBag(searchString, advanceSearch);
                return View(_serviceFactory.CreateVehicleDataService().GetAllVehiclesWithPagin(searchString, advanceSearch, pageNo, pageSize));
            } catch(NullReferenceException ne)
            {
                Utility.AlertMessage(this, "Data Issue. Please fill VehicleData in database", "alert-danger");
                return View();
            }
            catch(SqlException se)
            {
                Utility.AlertMessage(this, "Internal Server Error", "alert-danger");
                return View();
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

            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataById(Id);
            AddViewBag();
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

        public JsonResult GetYBSTypeByYBSCompanyId(int ybsCompanyId)
        {
            List<SelectListItem> ybsTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(ybsCompanyId);
            return Json(ybsTypes);
        }

    }
}

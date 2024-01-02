using ClosedXML.Excel;
using TransportationManagement.Classes;
using TransportationManagement.Factories;
using TransportationManagement.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Controllers.YBORecord
{
    public class YboRecordController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public YboRecordController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private void AddSearchDatasToViewBag(string searchString)
        {
            ViewBag.SearchString = searchString;
        }

        private string MakeExcelFileName(string searchString, bool ExportAll)
        {
            if (ExportAll)
            {
                return "YBOဖမ်းစီးမှုမှတ်တမ်းအားလုံး" + DateTime.Now + ".xlsx";
            }
            else
            {
                if (searchString != null && !string.IsNullOrEmpty(searchString))
                    return "YBOဖမ်းစီးမှုမှတ်တမ်းရှာဖွေမှု(" + searchString + ")" + DateTime.Now + ".xlsx";
                else
                    return "YBOဖမ်းစီးမှုမှတ်တမ်း" + DateTime.Now + ".xlsx";
            }
            
        }
        public IActionResult List(int? pageNo)
        {
            try
            {
                if (!SessionUtil.IsActiveSession(HttpContext))
                    return RedirectToAction("Index", "Login");

                string searchString = Request.Query["SearchString"];

                int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
                AddSearchDatasToViewBag(searchString);
                PagingList<YboRecord> yboRecords = _serviceFactory.CreateYBORecordService().GetAllYboRecordsWithPagin(searchString, pageNo, pageSize);
                if (Request.Query["export"] == "excel")
                {
                    bool ExportAll = Request.Query["ExportAll"] == "true";

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(_serviceFactory.CreateYBORecordService().MakeYboRecordExcelData(yboRecords, ExportAll));
                        ws.Rows().AdjustToContents();
                        ws.Rows().Height = 20;
                        ws.Columns().AdjustToContents();
                        ws.Columns().Style.Font.FontSize = 12;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", MakeExcelFileName(searchString, ExportAll));
                        }
                    }

                }
                return View(yboRecords);
            }
            catch (NullReferenceException ne)
            {
                Utility.AlertMessage(this, "Data Issue. Please fill YboRecord in database", "alert-danger");
                return View();
            }
            catch (SqlException se)
            {
                Utility.AlertMessage(this, "Internal Server Error", "alert-danger");
                return View();
            }
        }

        
        private void AddViewBag()
        {           
            ViewBag.YBSCompanies = _serviceFactory.CreateYBSCompanyService().GetSelectListYBSCompanys();
            ViewBag.YBSTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId();
        }
        public IActionResult Create()
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            AddViewBag();
            return View();
        }


        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(YboRecord yboRecord)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateYBORecordService().CreateYboRecord(yboRecord))
            {
                Utility.AlertMessage(this, "Save Success", "alert-success");
                try
                {
                    return RedirectToAction(nameof(List));
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill YboRecord in database", "alert-danger");
                    AddViewBag();
                    return View();
                }
                catch (SqlException se)
                {
                    Utility.AlertMessage(this, "Internal Server Error", "alert-danger");
                    AddViewBag();
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
            YboRecord yboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(Id);
            AddViewBag();
            return View(yboRecord);
        }

        public IActionResult Details(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            YboRecord yboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(Id);
            return View(yboRecord);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(YboRecord yboRecord)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateYBORecordService().EditYboRecord(yboRecord))
            {

                Utility.AlertMessage(this, "Edit Success", "alert-success");
                return RedirectToAction(nameof(List));
            }
            else
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                YboRecord record = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(yboRecord.YboRecordPkid);
                AddViewBag();
                return View(record);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            YboRecord yboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordById(Id);
            if (_serviceFactory.CreateYBORecordService().DeleteYboRecord(yboRecord))
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

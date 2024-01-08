using TransportationManagement.Factories;
using TransportationManagement.Models;
using TransportationManagement.Util;
using TransportationManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TransportationManagement.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TransportationManagement.Controllers.YBSTypeController
{
    public class YBSTypeController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public YBSTypeController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private YBSTypeView MakeYBSTypeView(int? pageNo, int ybsCompanyPkId)
        {
            List<SelectListItem> LstYBSCompanie = _serviceFactory.CreateYBSCompanyService().GetSelectListYBSCompanys();
            List<YBSType> YBSTypeList;
            PagingList<YBSType> paginYBSType;
            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            if (ybsCompanyPkId > 0)
            {
                YBSTypeList = _serviceFactory.CreateYBSTypeService().GetUniqueYBSTypesByYBSCompanyId(ybsCompanyPkId);
                paginYBSType = PagingList<YBSType>.CreateAsync(YBSTypeList.AsQueryable<YBSType>(), pageNo ?? 1, 100);

            }
            else
            {
                YBSTypeList = _serviceFactory.CreateYBSTypeService().GetUniqueYBSTypes();
                ybsCompanyPkId = 0;
                paginYBSType = PagingList<YBSType>.CreateAsync(YBSTypeList.AsQueryable<YBSType>(), pageNo ?? 1, pageSize);
            }
            
            YBSTypeView viewModel = new YBSTypeView
            {
                YBSCompanyList = LstYBSCompanie,
                YBSTypeList = paginYBSType,
                YBSType = new YBSType(),
                SelectedYBSCompany = ybsCompanyPkId
            };
            return viewModel;
        }
        public IActionResult List(int? pageNo, int ybsCompanyPkId)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            return View("CRUD", MakeYBSTypeView(pageNo, ybsCompanyPkId));
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(int ybsCompanyPkId, YBSType yBSType, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            if (_serviceFactory.CreateYBSTypeService().CreateYBSType(ybsCompanyPkId, yBSType))
            {
                try
                {
                    Utility.AlertMessage(this, "Save Success", "alert-success");
                    return RedirectToAction("List", new { pageNo = pageNo, ybsCompanyPkId = ybsCompanyPkId });
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill YBSCompany in database", "alert-danger");
                    return RedirectToAction("List", new { pageNo = pageNo, ybsCompanyPkId = ybsCompanyPkId });
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
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int ybsCompanyPkId, int Id, YBSType yBSType, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            yBSType.YBSTypePkid = Id;
            if (_serviceFactory.CreateYBSTypeService().EditYBSType(ybsCompanyPkId, yBSType))
            {
                Utility.AlertMessage(this, "Edit Success", "alert-success");
                return RedirectToAction("List", new { pageNo = pageNo, ybsCompanyPkId = ybsCompanyPkId });
            }
            else
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                return RedirectToAction("List", new { pageNo = pageNo, ybsCompanyPkId = ybsCompanyPkId });
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int ybsCompanyPkId, int Id, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            YBSType yBSType = _serviceFactory.CreateYBSTypeService().FindYBSTypeById(Id);
            if (_serviceFactory.CreateYBSTypeService().DeleteYBSType(yBSType))
            {
                Utility.AlertMessage(this, "Delete Success", "alert-success");
                return RedirectToAction("List", new { pageNo = pageNo, ybsCompanyPkId = ybsCompanyPkId });
            }
            else
            {
                Utility.AlertMessage(this, "Delete Fail.Internal Server Error", "alert-danger");
                return RedirectToAction("List", new { pageNo = pageNo, ybsCompanyPkId = ybsCompanyPkId });
            }
        }

        public JsonResult GetYBSTypeByYBSCompanyId(int ybsCompanyId)
        {
            List<SelectListItem> ybsTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(ybsCompanyId);
            return Json(ybsTypes);
        }
    }
}

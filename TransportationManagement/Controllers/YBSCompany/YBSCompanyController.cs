using TransportationManagement.Factories;
using TransportationManagement.Models;
using TransportationManagement.Util;
using TransportationManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TransportationManagement.Paging;

namespace TransportationManagement.Controllers.YBSCompanyController
{
    public class YBSCompanyController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public YBSCompanyController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private YBSCompanyView MakeYBSCompanyView(int? pageNo)
        {
            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            PagingList<YBSCompany> yBSCompanies = _serviceFactory.CreateYBSCompanyService().GetAllYBSCompanysWithPagin(pageNo, pageSize);
            if(yBSCompanies.Count() < 1)
            {
                int newPageNo = pageNo.HasValue ? pageNo.GetValueOrDefault() - 1 : 1;//pageNo.HasValue ? (pageNo.GetValueOrDefault() - 1) : pageNo.Value;
                yBSCompanies = _serviceFactory.CreateYBSCompanyService().GetAllYBSCompanysWithPagin(newPageNo, pageSize);
                yBSCompanies.PageIndex = newPageNo;
            }
            YBSCompanyView viewModel = new YBSCompanyView
            {
                YBSCompanyList = yBSCompanies,
                YBSCompany = new YBSCompany()
            };
            return viewModel;
        }
        public IActionResult List(int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            return View("CRUD", MakeYBSCompanyView(pageNo));
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(YBSCompany yBSCompany, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateYBSCompanyService().CreateYBSCompany(yBSCompany))
            {
                Utility.AlertMessage(this, "Save Success", "alert-success");
                try
                {
                    return RedirectToAction("List", new { pageNo = pageNo });
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill YBSCompany in database", "alert-danger");
                    return RedirectToAction("List", new { pageNo = pageNo });
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
        public IActionResult Edit(int Id, YBSCompany yBSCompany, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            yBSCompany.YBSCompanyPkid = Id;
            if (_serviceFactory.CreateYBSCompanyService().EditYBSCompany(yBSCompany))
            {
                Utility.AlertMessage(this, "Edit Success", "alert-success");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
            else
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int Id, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            YBSCompany yBSCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyById(Id);
            if (_serviceFactory.CreateYBSCompanyService().DeleteYBSCompany(yBSCompany))
            {
                Utility.AlertMessage(this, "Delete Success", "alert-success");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
            else
            {
                Utility.AlertMessage(this, "Delete Fail.Internal Server Error", "alert-danger");
                return RedirectToAction("List", new { pageNo = pageNo });
            }
        }
    }
}

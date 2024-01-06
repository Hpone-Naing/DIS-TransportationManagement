using TransportationManagement.Factories;
using TransportationManagement.Models;
using TransportationManagement.Util;
using TransportationManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TransportationManagement.Paging;

namespace TransportationManagement.Controllers.FuelTypeController
{
    public class FuelTypeController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public FuelTypeController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private FuelTypeView MakeFuelTypeView(int? pageNo)
        {
            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            PagingList<FuelType> fuelTypes = _serviceFactory.CreateFuelTypeService().GetAllFuelTypesWithPagin(pageNo, pageSize);
            if(fuelTypes.Count() < 1)
            {
                int newPageNo = pageNo.HasValue ? pageNo.GetValueOrDefault() - 1 : 1;//pageNo.HasValue ? (pageNo.GetValueOrDefault() - 1) : pageNo.Value;
                fuelTypes = _serviceFactory.CreateFuelTypeService().GetAllFuelTypesWithPagin(newPageNo, pageSize);
                fuelTypes.PageIndex = newPageNo;
            }
            FuelTypeView viewModel = new FuelTypeView
            {
                FuelTypeList = fuelTypes,
                FuelType = new FuelType()
            };
            return viewModel;
        }
        public IActionResult List(int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            return View("CRUD", MakeFuelTypeView(pageNo));
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(FuelType fuelType, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateFuelTypeService().CreateFuelType(fuelType))
            {
                Utility.AlertMessage(this, "Save Success", "alert-success");
                try
                {
                    return RedirectToAction("List", new { pageNo = pageNo });
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill FuelType in database", "alert-danger");
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
        public IActionResult Edit(int Id, FuelType fuelType, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            fuelType.FuelTypePkid = Id;
            if (_serviceFactory.CreateFuelTypeService().EditFuelType(fuelType))
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

            FuelType fuelType = _serviceFactory.CreateFuelTypeService().FindFuelTypeById(Id);
            if (_serviceFactory.CreateFuelTypeService().DeleteFuelType(fuelType))
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

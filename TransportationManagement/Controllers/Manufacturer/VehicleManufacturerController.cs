using TransportationManagement.Factories;
using TransportationManagement.Models;
using TransportationManagement.Util;
using TransportationManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TransportationManagement.Paging;

namespace TransportationManagement.Controllers.VehicleManufacturer
{
    public class VehicleManufacturerController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public VehicleManufacturerController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private ManufacturerView MakeManufacturerView(int? pageNo)
        {
            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            PagingList<Manufacturer> manufacturers = _serviceFactory.CreateManufacturerService().GetAllManufacturersWithPagin(pageNo, pageSize);
            if(manufacturers.Count() < 1)
            {
                int newPageNo = pageNo.HasValue ? pageNo.GetValueOrDefault() - 1 : 1;//pageNo.HasValue ? (pageNo.GetValueOrDefault() - 1) : pageNo.Value;
                manufacturers = _serviceFactory.CreateManufacturerService().GetAllManufacturersWithPagin(newPageNo, pageSize);
                manufacturers.PageIndex = newPageNo;
            }
            ManufacturerView viewModel = new ManufacturerView
            {
                ManufacturerList = manufacturers,
                Manufacturer = new Manufacturer()
            };
            return viewModel;
        }
        public IActionResult List(int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            return View("CRUD", MakeManufacturerView(pageNo));
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(Manufacturer manufacturer, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateManufacturerService().CreateManufacturer(manufacturer))
            {
                Utility.AlertMessage(this, "Save Success", "alert-success");
                try
                {
                    return RedirectToAction("List", new { pageNo = pageNo });
                }
                catch (NullReferenceException ne)
                {
                    Utility.AlertMessage(this, "Data Issue. Please fill Manufacturer in database", "alert-danger");
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
        public IActionResult Edit(int Id, Manufacturer manufacturer, int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            manufacturer.ManufacturerPkid = Id;
            if (_serviceFactory.CreateManufacturerService().EditManufacturer(manufacturer))
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

            Manufacturer manufacturer = _serviceFactory.CreateManufacturerService().FindManufacturerById(Id);
            if (_serviceFactory.CreateManufacturerService().DeleteManufacturer(manufacturer))
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

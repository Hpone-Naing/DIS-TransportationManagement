using TransportationManagement.Classes;
using TransportationManagement.Data;
using TransportationManagement.Paging;
using Microsoft.EntityFrameworkCore;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class VehicleDataServiceImpl : AbstractServiceImpl<VehicleData>, VehicleDataService
    {
        public VehicleDataServiceImpl(HumanResourceManagementDBContext context) : base(context)
        {
        }

        public List<VehicleData> GetAllVehicles()
        {
            return GetAll().Where(vehicle => vehicle.IsDeleted == false).ToList();
        }

        public PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize)
        {
            List<VehicleData> vehicleDatas = GetAllVehicles();
            List<VehicleData> resultList = new List<VehicleData>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
                resultList = vehicleDatas.Where(vehicle => IsSearchDataContained(vehicle, searchString))
                            .AsQueryable()
                            .ToList();
            }
            else if (advanceSearch.POSInstalled != null || advanceSearch.CctvInstalled != null || advanceSearch.TotalBusStop != null)
            {
                resultList = AdvanceSearch(advanceSearch, _context.VehicleDatas).Where(vehicleData => !vehicleData.IsDeleted).ToList();
            }
            else
            {
                resultList = vehicleDatas;
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }

        public bool CreateVehicle(VehicleData vehicleData)
        {
            vehicleData.IsDeleted = false;
            vehicleData.CreatedDate = DateTime.Now;
            vehicleData.RegistrationDate = DateTime.Now;
            return Create(vehicleData);
        }

        public bool EditVehicle(VehicleData vehicleData)
        {
            return Update(vehicleData);
        }

        public bool DeleteVehicle(VehicleData vehicleData)
        {
            vehicleData.IsDeleted = true;
            return Update(vehicleData);
        }

        public VehicleData FindVehicleDataById(int id)
        {
            return FindById(id);
        }

        public VehicleData FindVehicleDataByIdEgerLoad(int id)
        {
            return _context.VehicleDatas
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .Include(vehicle => vehicle.Manufacturer)
                           .Include(vehicle => vehicle.FuelType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
        }
    }
}

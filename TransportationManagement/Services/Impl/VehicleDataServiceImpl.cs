using TransportationManagement.Classes;
using TransportationManagement.Data;
using TransportationManagement.Paging;
using Microsoft.EntityFrameworkCore;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class VehicleDataServiceImpl : AbstractServiceImpl<VehicleData>, VehicleDataService
    {
        private readonly ILogger<VehicleDataServiceImpl> _logger;

        public VehicleDataServiceImpl(HumanResourceManagementDBContext context, ILogger<VehicleDataServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<VehicleData> GetAllVehicles()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicles] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return GetAll().Where(vehicle => vehicle.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehiclesWithPagin] SearchAll or AdvanceSearch or GetAll VehicleData paginate eger load list. <<<<<<<<<<");
            try
            {
                List<VehicleData> vehicleDatas = GetAllVehicles();
                List<VehicleData> resultList = new List<VehicleData>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = vehicleDatas.Where(vehicle => IsSearchDataContained(vehicle, searchString))
                            .AsQueryable()
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else if (!string.IsNullOrEmpty(advanceSearch.POSInstalled) || !string.IsNullOrEmpty(advanceSearch.TelematicDeviceInstalled) || !string.IsNullOrEmpty(advanceSearch.CctvInstalled))
                {
                    _logger.LogInformation($">>>>>>>>>> Get AdvanceSearch result VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get AdvanceSearch result VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = AdvanceSearch(advanceSearch, _context.VehicleDatas).Where(vehicleData => !vehicleData.IsDeleted).ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get AdvanceSearch result VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = vehicleDatas;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> Success. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or AdvanceSearch or GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][CreateVehicle] Create VehicleData. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Create VehicleData. <<<<<<<<<<");
                vehicleData.IsDeleted = false;
                vehicleData.CreatedDate = DateTime.Now;
                vehicleData.RegistrationDate = DateTime.Now;
                return Create(vehicleData);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating VehicleData. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][EditVehicle] Edit VehicleData. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit VehicleData. <<<<<<<<<<");
                return Update(vehicleData);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating VehicleData. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][DeleteVehicle] Soft delete VehicleData. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete VehicleData. <<<<<<<<<<");
                vehicleData.IsDeleted = true;
                return Update(vehicleData);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting VehicleData. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataById] Find VehicleData by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByIdEgerLoad] Find VehicleData by pkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId with eger load. <<<<<<<<<<");
                return _context.VehicleDatas
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .Include(vehicle => vehicle.Manufacturer)
                           .Include(vehicle => vehicle.FuelType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

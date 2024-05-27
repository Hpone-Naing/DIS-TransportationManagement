using TransportationManagement.Data;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class FuelTypeServiceImpl : AbstractServiceImpl<FuelType>, FuelTypeService
    {
        private readonly ILogger<FuelTypeServiceImpl> _logger;

        public FuelTypeServiceImpl(HumanResourceManagementDBContext context, ILogger<FuelTypeServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<FuelType> GetAllFuelTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][GetAllFuelTypes] GetAll FuelType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. GetAll FuelType. <<<<<<<<<<");
                return GetAll().Where(fuelType => !fuelType.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing FuelType. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<FuelType> GetAllFuelTypesWithPagin(int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][GetAllFuelTypesWithPagin] Get FuelType pagination list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get FuelType pagination list. <<<<<<<<<<");

                return GetAllWithPagin(GetAllFuelTypes(), pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing FuelType pagination list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<FuelType> GetUniqueFuelTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][GetUniqueFuelTypes] Get unique FuelType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique FuelType list. <<<<<<<<<<");
                return GetUniqueList(fuelType => fuelType.FuelTypePkid).Where(fuelType => !fuelType.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique FuelType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public FuelType FindFuelTypeById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][FindFuelTypeById] Get FuelType by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get FuelType by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting FuelType by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public FuelType FindFuelTypeByName(string name)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][FindFuelTypeByName] Get FuelType by name. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get FuelType by name. <<<<<<<<<<");
                return FindByString("FuelTypeName", name);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting FuelType by name. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateFuelType(FuelType fuelType)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][CreateFuelType] Create FuelType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Create FuelType. <<<<<<<<<<");
                fuelType.IsDeleted = false;
                return Create(fuelType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating FuelType. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool EditFuelType(FuelType fuelType)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][EditFuelType] Edit FuelType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit FuelType. <<<<<<<<<<");
                return Update(fuelType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating FuelType. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteFuelType(FuelType fuelType)
        {
            _logger.LogInformation(">>>>>>>>>> [FuelTypeServiceImpl][DeleteFuelType] Soft delete FuelType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete FuelType. <<<<<<<<<<");
                fuelType.IsDeleted = true;
                return Update(fuelType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting FuelType. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

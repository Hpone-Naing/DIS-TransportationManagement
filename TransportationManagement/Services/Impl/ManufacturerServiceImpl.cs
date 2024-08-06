using Microsoft.Extensions.Logging;
using TransportationManagement.Data;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Services.Impl
{
    public class ManufacturerServiceImpl : AbstractServiceImpl<Manufacturer>, ManufacturerService
    {
        private readonly ILogger<ManufacturerServiceImpl> _logger;

        public ManufacturerServiceImpl(HumanResourceManagementDBContext context, ILogger<ManufacturerServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][GetAllManufacturers] Get Manufacturer list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get Manufacturer list. <<<<<<<<<<");
                return GetAll().Where(manufacturer => manufacturer.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving Manufacturer list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<Manufacturer> GetAllManufacturersWithPagin(int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][GetAllManufacturersWithPagin] Get Manufacturer pagination list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get Manufacturer pagination list. <<<<<<<<<<");
                List<Manufacturer> manufacturers = _context.Manufacturers
                                             .Where(manufacturer => !manufacturer.IsDeleted)
                                             .GroupBy(manufacturer => manufacturer.ManufacturerPkid)
                                             .Select(g => g.First())
                                             .ToList();
                var vehicleDataCount = _context.VehicleDatas
                    .GroupBy(vehicle => vehicle.VehicleManufacturer)
                    .Select(group => new { ManufacturerPkid = group.Key, Count = group.Count() })
                    .ToList();

                foreach (var manufacturer in manufacturers)
                {
                    var ybsTypeCount = vehicleDataCount.FirstOrDefault(x => x.ManufacturerPkid == manufacturer.ManufacturerPkid);
                    manufacturer.TotalYBSNumber = ybsTypeCount != null ? ybsTypeCount.Count : 0;
                }
                return GetAllWithPagin(manufacturers, pageNo, PageSize);
                //return GetAllWithPagin(GetAllManufacturers(), pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing Manufacturer pagination list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<Manufacturer> GetUniqueManufacturers()
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][GetUniqueManufacturers] Get unique Manufacturer list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique Manufacturer list. <<<<<<<<<<");
                return GetUniqueList(manufacturer => manufacturer.ManufacturerPkid).Where(manufacturer => !manufacturer.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique Manufacturer list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public Manufacturer FindManufacturerById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][FindManufacturerById] Find Manufacturer by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find Manufacturer by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding Manufacturer by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public Manufacturer FindManufacturerByName(string name)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][FindManufacturerByName] Find Manufacturer by name. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find Manufacturer by name. <<<<<<<<<<");
                return FindByString("ManufacturerName", name);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding Manufacturer by name. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateManufacturer(Manufacturer manufacturer)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][CreateManufacturer] Create Manufacturer. <<<<<<<<<<");
            try
            {
                manufacturer.IsDeleted = false;
                _logger.LogInformation($">>>>>>>>>> Success. Create Manufacturer. <<<<<<<<<<");
                return Create(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating Manufacturer. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool EditManufacturer(Manufacturer manufacturer)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][EditManufacturer] Edit Manufacturer. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit Manufacturer. <<<<<<<<<<");
                return Update(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating Manufacturer. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            _logger.LogInformation(">>>>>>>>>> [ManufacturerServiceImpl][DeleteManufacturer] Soft delete Manufacturer. <<<<<<<<<<");
            try
            { 
                manufacturer.IsDeleted = true;
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete Manufacturer. <<<<<<<<<<");
                return Update(manufacturer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting Manufacturer. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

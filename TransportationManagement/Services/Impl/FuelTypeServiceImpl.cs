using TransportationManagement.Data;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class FuelTypeServiceImpl : AbstractServiceImpl<FuelType>, FuelTypeService
    {
        public FuelTypeServiceImpl(HumanResourceManagementDBContext context) : base(context)
        {
        }

        public List<FuelType> GetAllFuelTypes()
        {
            return GetAll().Where(fuelType => !fuelType.IsDeleted).ToList();
        }

        public PagingList<FuelType> GetAllFuelTypesWithPagin(int? pageNo, int PageSize)
        {
            return GetAllWithPagin(GetAllFuelTypes(), pageNo, PageSize);
        }

        public List<FuelType> GetUniqueFuelTypes()
        {
            return GetUniqueList(fuelType => fuelType.FuelTypePkid).Where(fuelType => !fuelType.IsDeleted).ToList();
        }

        public FuelType FindFuelTypeById(int id)
        {
            return FindById(id);
        }

        public bool CreateFuelType(FuelType fuelType)
        {
            fuelType.IsDeleted = false;
            return Create(fuelType);
        }
        public bool EditFuelType(FuelType fuelType)
        {
            return Update(fuelType);
        }

        public bool DeleteFuelType(FuelType fuelType)
        {
            fuelType.IsDeleted = true;
            return Update(fuelType);
        }
    }
}

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
            return GetAll();
        }

        public List<FuelType> GetUniqueFuelTypes()
        {
            return GetUniqueList(fuelType => fuelType.FuelTypePkid);
        }

        public bool CreateFuelType(FuelType fuelType)
        {
            return Create(fuelType);
        }
    }
}

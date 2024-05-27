using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface FuelTypeService
    {
        List<FuelType> GetUniqueFuelTypes();
        public PagingList<FuelType> GetAllFuelTypesWithPagin(int? pageNo, int PageSize);
        public FuelType FindFuelTypeById(int id);
        public FuelType FindFuelTypeByName(string name);
        public bool CreateFuelType(FuelType manufacturer);
        public bool DeleteFuelType(FuelType manufacturer);
        public bool EditFuelType(FuelType manufacturer);
    }
}

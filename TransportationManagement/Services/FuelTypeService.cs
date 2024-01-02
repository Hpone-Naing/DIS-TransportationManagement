using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface FuelTypeService
    {
        List<FuelType> GetUniqueFuelTypes();
    }
}

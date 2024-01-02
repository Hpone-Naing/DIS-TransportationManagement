using TransportationManagement.Classes;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Services
{
    public interface VehicleDataService
    {
        bool CreateVehicle(VehicleData vehicleData);
        List<VehicleData> GetAllVehicles();
        PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize);
        bool DeleteVehicle(VehicleData vehicleData);
        VehicleData FindVehicleDataById(int id);
        VehicleData FindVehicleDataByIdEgerLoad(int id);
        bool EditVehicle(VehicleData vehicleData);
    }
}

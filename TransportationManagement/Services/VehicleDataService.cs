using System.Data;
using TransportationManagement.Classes;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Services
{
    public interface VehicleDataService
    {
        bool CreateVehicle(VehicleData vehicleData);
        List<VehicleData> GetAllVehicles();
        PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string? searchOption = "");

        public PagingList<VehicleData> GetAllVehiclesWithPaginForExcelExport(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize);
        bool DeleteVehicle(VehicleData vehicleData);
        bool HardDeleteVehicle(VehicleData vehicleData);
        VehicleData FindVehicleDataById(int id);
        public VehicleData FindVehicleDataByVehicleNumber(string vehicleNumber);
        VehicleData FindVehicleDataByIdEgerLoad(int id);
        bool EditVehicle(VehicleData vehicleData);
        public DataTable MakeVehicleDataExcelData(PagingList<VehicleData> vehicleDatas, bool exportAll);
    }
}

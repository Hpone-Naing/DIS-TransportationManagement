using TransportationManagement.Services;

namespace TransportationManagement.Factories
{
    public interface ServiceFactory
    {
        UserService CreateUserService();
        EmployeeService CreateEmployeeService();
        VehicleDataService CreateVehicleDataService();
        FuelTypeService CreateFuelTypeService();
        ManufacturerService CreateManufacturerService();
        YBSCompanyService CreateYBSCompanyService();
        YBSTypeService CreateYBSTypeService();

        YboRecordService CreateYBORecordService();
        DriverService CreateDriverService();
    }
}

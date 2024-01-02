using TransportationManagement.Data;
using TransportationManagement.Services;
using TransportationManagement.Services.Impl;

namespace TransportationManagement.Factories.Impl
{
    public class ServiceFactoryImpl : ServiceFactory
    {
        private readonly HumanResourceManagementDBContext _context;
        private readonly DriverService _driverService;

        public ServiceFactoryImpl(HumanResourceManagementDBContext context, DriverService driverService)
        {
            _context = context;
            _driverService = driverService;
        }

        public UserService CreateUserService()
        {
            return new UserServiceImpl(_context);
        }
        public EmployeeService CreateEmployeeService()
        {
            return new EmployeeServiceImpl();
        }

        public VehicleDataService CreateVehicleDataService()
        {
            return new VehicleDataServiceImpl(_context);
        }
        public FuelTypeService CreateFuelTypeService()
        {
            return new FuelTypeServiceImpl(_context);
        }
        public ManufacturerService CreateManufacturerService()
        {
            return new ManufacturerServiceImpl(_context);
        }
        public YBSCompanyService CreateYBSCompanyService()
        {
            return new YBSCompanyServiceImpl(_context);
        }
        public YBSTypeService CreateYBSTypeService()
        {
            return new YBSTypeServiceImpl(_context);
        }
        public DriverService CreateDriverService()
        {
            return new DriverServiceImpl(_context);
        }

        public YboRecordService CreateYBORecordService()
        {
            return new YboServiceImpl(_context, _driverService);
        }

    }
}

using TransportationManagement.Data;
using TransportationManagement.Services;
using TransportationManagement.Services.Impl;

namespace TransportationManagement.Factories.Impl
{
    public class ServiceFactoryImpl : ServiceFactory
    {
        private readonly HumanResourceManagementDBContext _context;
        private readonly DriverService _driverService;
        private readonly YBSCompanyService _ybsCompanyService;

        public ServiceFactoryImpl(HumanResourceManagementDBContext context, DriverService driverService, YBSCompanyService ybsCompanyService)
        {
            _context = context;
            _driverService = driverService;
            _ybsCompanyService = ybsCompanyService;
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
            return new YBSTypeServiceImpl(_context, _ybsCompanyService);
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

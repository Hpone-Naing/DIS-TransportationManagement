using TransportationManagement.Data;
using TransportationManagement.Services;
using TransportationManagement.Services.Impl;

namespace TransportationManagement.Factories.Impl
{
    public class ServiceFactoryImpl : ServiceFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        private readonly HumanResourceManagementDBContext _context;
        private readonly DriverService _driverService;
        private readonly YBSCompanyService _ybsCompanyService;

        public ServiceFactoryImpl(HumanResourceManagementDBContext context, ILoggerFactory loggerFactory, DriverService driverService, YBSCompanyService ybsCompanyService)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _driverService = driverService;
            _ybsCompanyService = ybsCompanyService;
        }

        public UserService CreateUserService()
        {
            ILogger<UserServiceImpl> userLogger = new Logger<UserServiceImpl>(_loggerFactory);
            return new UserServiceImpl(_context, userLogger);
        }
        public EmployeeService CreateEmployeeService()
        {
            return new EmployeeServiceImpl();
        }

        public VehicleDataService CreateVehicleDataService()
        {
            ILogger<VehicleDataServiceImpl> vehicleDataLogger = new Logger<VehicleDataServiceImpl>(_loggerFactory);
            return new VehicleDataServiceImpl(_context, vehicleDataLogger);
        }
        public FuelTypeService CreateFuelTypeService()
        {
            ILogger<FuelTypeServiceImpl> fuelTypeLogger = new Logger<FuelTypeServiceImpl>(_loggerFactory);
            return new FuelTypeServiceImpl(_context, fuelTypeLogger);
        }
        public ManufacturerService CreateManufacturerService()
        {
            ILogger<ManufacturerServiceImpl> manufacturerLogger = new Logger<ManufacturerServiceImpl>(_loggerFactory);
            return new ManufacturerServiceImpl(_context, manufacturerLogger);
        }
        public YBSCompanyService CreateYBSCompanyService()
        {
            ILogger<YBSCompanyServiceImpl> ybsCompanyLogger = new Logger<YBSCompanyServiceImpl>(_loggerFactory);
            return new YBSCompanyServiceImpl(_context, ybsCompanyLogger);
        }
        public YBSTypeService CreateYBSTypeService()
        {
            ILogger<YBSTypeServiceImpl> ybsTypeLogger = new Logger<YBSTypeServiceImpl>(_loggerFactory);
            return new YBSTypeServiceImpl(_context, ybsTypeLogger, _ybsCompanyService);
        }
        public DriverService CreateDriverService()
        {
            ILogger<DriverServiceImpl> driverLogger = new Logger<DriverServiceImpl>(_loggerFactory);
            return new DriverServiceImpl(_context,driverLogger);
        }

        public YboRecordService CreateYBORecordService()
        {
            ILogger<YboServiceImpl> yboServiceLogger = new Logger<YboServiceImpl>(_loggerFactory);
            return new YboServiceImpl(_context, _driverService, yboServiceLogger);
        }

    }
}

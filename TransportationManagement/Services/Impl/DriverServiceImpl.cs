using TransportationManagement.Data;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class DriverServiceImpl : AbstractServiceImpl<Driver>, DriverService
    {
        private readonly ILogger<DriverServiceImpl> _logger;

        public DriverServiceImpl(HumanResourceManagementDBContext context, ILogger<DriverServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public Driver FindDriverByLicense(string licenseNumber)
        {
            return FindByString("DriverLicense", licenseNumber);
        }

        public bool CreateDriver(Driver driver)
        {
            return Create(driver);
        }

        public bool EditDriver(Driver driver)
        {
            return Update(driver);
        }
    }
}

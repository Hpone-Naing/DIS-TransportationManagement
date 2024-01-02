using TransportationManagement.Data;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class DriverServiceImpl : AbstractServiceImpl<Driver>, DriverService
    {
        public DriverServiceImpl(HumanResourceManagementDBContext context) : base(context)
        {
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

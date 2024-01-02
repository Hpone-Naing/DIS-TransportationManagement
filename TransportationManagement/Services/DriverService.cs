using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface DriverService
    {
        public Driver FindDriverByLicense(string licenseNumber);

        public bool CreateDriver(Driver driver);

        public bool EditDriver(Driver driver);
    }
}

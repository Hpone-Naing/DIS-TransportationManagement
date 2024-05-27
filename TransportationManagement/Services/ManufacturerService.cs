using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Services
{
    public interface ManufacturerService
    {
        List<Manufacturer> GetUniqueManufacturers();
        public PagingList<Manufacturer> GetAllManufacturersWithPagin(int? pageNo, int PageSize);
        public Manufacturer FindManufacturerById(int id);
        public Manufacturer FindManufacturerByName(string name);
        public bool CreateManufacturer(Manufacturer manufacturer);
        public bool DeleteManufacturer(Manufacturer manufacturer);
        public bool EditManufacturer(Manufacturer manufacturer);



    }
}

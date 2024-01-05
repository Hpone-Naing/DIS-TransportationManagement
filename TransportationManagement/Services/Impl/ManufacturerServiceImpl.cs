using TransportationManagement.Data;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Services.Impl
{
    public class ManufacturerServiceImpl : AbstractServiceImpl<Manufacturer>, ManufacturerService
    {
        public ManufacturerServiceImpl(HumanResourceManagementDBContext context) : base(context)
        {
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return GetAll().Where(manufacturer => manufacturer.IsDeleted == false).ToList();
        }

        public PagingList<Manufacturer> GetAllManufacturersWithPagin(int? pageNo, int PageSize)
        {
            return GetAllWithPagin(GetAllManufacturers(), pageNo, PageSize);
        }

        public List<Manufacturer> GetUniqueManufacturers()
        {
            return GetUniqueList(manufacturer => manufacturer.ManufacturerPkid).Where(manufacturer => !manufacturer.IsDeleted).ToList();
        }

        public Manufacturer FindManufacturerById(int id)
        {
            return FindById(id);
        }

        public bool CreateManufacturer(Manufacturer manufacturer)
        {
            manufacturer.IsDeleted = false;
            return Create(manufacturer);
        }
        public bool EditManufacturer(Manufacturer manufacturer)
        {
            return Update(manufacturer);
        }

        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            manufacturer.IsDeleted = true;
            return Update(manufacturer);
        }
    }
}

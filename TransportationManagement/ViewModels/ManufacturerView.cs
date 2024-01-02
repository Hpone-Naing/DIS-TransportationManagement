
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.ViewModels
{
    public class ManufacturerView
    {
        public PagingList<Manufacturer> ManufacturerList { get; set; }
        public Manufacturer Manufacturer{ get; set; }
    }
}

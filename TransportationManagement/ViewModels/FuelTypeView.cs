using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.ViewModels
{
    public class FuelTypeView
    {
        public PagingList<FuelType> FuelTypeList { get; set; }
        public FuelType FuelType{ get; set; }
    }
}

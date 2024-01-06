
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.ViewModels
{
    public class YBSCompanyView
    {
        public PagingList<YBSCompany> YBSCompanyList { get; set; }
        public YBSCompany YBSCompany{ get; set; }
    }
}

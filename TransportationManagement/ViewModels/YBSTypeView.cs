
using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.ViewModels
{
    public class YBSTypeView
    {
        public PagingList<YBSType> YBSTypeList { get; set; }
        public List<SelectListItem> YBSCompanyList { get; set; }
        public YBSType YBSType{ get; set; }
        public int SelectedYBSCompany{ get; set; }
    }
}

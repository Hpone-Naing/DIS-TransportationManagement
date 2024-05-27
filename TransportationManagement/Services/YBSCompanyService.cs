using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface YBSCompanyService
    {
        List<YBSCompany> GetUniqueYBSCompanys();
        List<SelectListItem> GetSelectListYBSCompanys();
        public PagingList<YBSCompany> GetAllYBSCompanysWithPagin(int? pageNo, int PageSize);
        public YBSCompany FindYBSCompanyById(int id);
        public YBSCompany FindYBSCompanyByName(string name);

        public bool CreateYBSCompany(YBSCompany manufacturer);
        public bool DeleteYBSCompany(YBSCompany manufacturer);
        public bool EditYBSCompany(YBSCompany manufacturer);

    }
}

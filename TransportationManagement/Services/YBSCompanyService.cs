using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface YBSCompanyService
    {
        List<YBSCompany> GetUniqueYBSCompanys();
        List<SelectListItem> GetSelectListYBSCompanys();

    }
}

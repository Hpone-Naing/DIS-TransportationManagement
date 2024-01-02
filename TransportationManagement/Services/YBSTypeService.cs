using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface YBSTypeService
    {
        List<YBSType> GetUniqueYBSTypes();
        List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1);
        List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1);
    }
}

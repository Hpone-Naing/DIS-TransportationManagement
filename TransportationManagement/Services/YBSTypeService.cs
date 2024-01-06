using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;

namespace TransportationManagement.Services
{
    public interface YBSTypeService
    {
        List<YBSType> GetUniqueYBSTypes();
        List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1);
        List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1);
        public PagingList<YBSType> GetAllYBSTypesWithPagin(int? pageNo, int PageSize);
        List<YBSType> GetAllYBSTypes();
        public YBSType FindYBSTypeById(int id);
        bool CreateYBSType(int ybsCompanyPkId, YBSType yBSType);
        public bool EditYBSType(int ybsCompanyPkId, YBSType yBSType);
        bool DeleteYBSType(YBSType yBSType);



    }
}

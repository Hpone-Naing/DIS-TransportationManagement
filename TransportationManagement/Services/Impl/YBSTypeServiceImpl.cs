using TransportationManagement.Data;
using TransportationManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TransportationManagement.Services.Impl
{
    public class YBSTypeServiceImpl : AbstractServiceImpl<YBSType>, YBSTypeService
    {
        private readonly YBSCompanyService _ybsCompanyService;
        public YBSTypeServiceImpl(HumanResourceManagementDBContext context, YBSCompanyService ybsCompanyService) : base(context)
        {
            _ybsCompanyService = ybsCompanyService;
        }

        public List<YBSType> GetAllYBSTypes()
        {
            return GetAll().Where(ybsType => !ybsType.IsDeleted).ToList();
        }

        public List<YBSType> GetUniqueYBSTypes()
        {
            return GetUniqueList(ybsType => ybsType.YBSTypePkid).Where(ybsType => !ybsType.IsDeleted).ToList();
        }

        public List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            return GetListByIntVal("YBSCompanyPkid", ybsCompanyId).Where(ybsType => !ybsType.IsDeleted).ToList();
        }

        public PagingList<YBSType> GetAllYBSTypesWithPagin(int? pageNo, int PageSize)
        {
            return GetAllWithPagin(GetAllYBSTypes(), pageNo, PageSize);
        }

        public List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            List<SelectListItem> lstYBSTypes = GetUniqueYBSTypesByYBSCompanyId(ybsCompanyId)
                .Select(
                    ybsType => new SelectListItem
                    {
                        Value = ybsType.YBSTypePkid.ToString(),
                        Text = ybsType.YBSTypeName
                    }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-----ရွေးချယ်ရန်-----"
            };

            lstYBSTypes.Insert(0, defItem);
            return lstYBSTypes;
        }

        public YBSType FindYBSTypeById(int id)
        {
            return FindById(id);
        }

        public bool CreateYBSType(int ybsCompanyPkId, YBSType yBSType)
        {
            YBSCompany ybsCompany = _ybsCompanyService.FindYBSCompanyById(ybsCompanyPkId);
            yBSType.YBSCompany = ybsCompany;
            yBSType.IsDeleted = false;
            return Create(yBSType);
        }
        public bool EditYBSType(int ybsCompanyPkId, YBSType yBSType)
        {
            YBSCompany ybsCompany = _ybsCompanyService.FindYBSCompanyById(ybsCompanyPkId);
            yBSType.YBSCompany = ybsCompany;
            return Update(yBSType);
        }

        public bool DeleteYBSType(YBSType yBSType)
        {
            yBSType.IsDeleted = true;
            return Update(yBSType);
        }
    }
}

using TransportationManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class YBSCompanyServiceImpl : AbstractServiceImpl<YBSCompany>, YBSCompanyService
    {
        public YBSCompanyServiceImpl(HumanResourceManagementDBContext context) : base(context)
        {
        }

        public List<YBSCompany> GetAllYBSCompanys()
        {
            return GetAll().Where(ybsCompany => !ybsCompany.IsDeleted).ToList();
        }

        public List<SelectListItem> GetSelectListYBSCompanys()
        {
            var lstYBSCompanys = new List<SelectListItem>();
            List<YBSCompany> YBSCompanys = GetUniqueYBSCompanys();
            lstYBSCompanys = YBSCompanys.Select(ybsCompany => new SelectListItem()
            {
                Value = ybsCompany.YBSCompanyPkid.ToString(),
                Text = ybsCompany.YBSCompanyName
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-----ရွေးချယ်ရန်-----"
            };

            lstYBSCompanys.Insert(0, defItem);
            return lstYBSCompanys;
        }

        

        public PagingList<YBSCompany> GetAllYBSCompanysWithPagin(int? pageNo, int PageSize)
        {
            return GetAllWithPagin(GetAllYBSCompanys(), pageNo, PageSize);
        }

        public List<YBSCompany> GetUniqueYBSCompanys()
        {
            return GetUniqueList(yBSCompany => yBSCompany.YBSCompanyPkid).Where(yBSCompany => !yBSCompany.IsDeleted).ToList();
        }

        public YBSCompany FindYBSCompanyById(int id)
        {
            return FindById(id);
        }

        public bool CreateYBSCompany(YBSCompany yBSCompany)
        {
            yBSCompany.IsDeleted = false;
            return Create(yBSCompany);
        }
        public bool EditYBSCompany(YBSCompany yBSCompany)
        {
            return Update(yBSCompany);
        }

        public bool DeleteYBSCompany(YBSCompany yBSCompany)
        {
            yBSCompany.IsDeleted = true;
            return Update(yBSCompany);
        }
    }
}

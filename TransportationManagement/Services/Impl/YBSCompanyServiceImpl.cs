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
            return GetAll();
        }

        public List<YBSCompany> GetUniqueYBSCompanys()
        {
            return GetUniqueList(ybscompany => ybscompany.YBSCompanyPkid);
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

        public bool CreateYBSCompany(YBSCompany ybsCompany)
        {
            return Create(ybsCompany);
        }
    }
}

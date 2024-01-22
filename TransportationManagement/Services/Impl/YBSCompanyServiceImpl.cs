using TransportationManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using TransportationManagement.Models;

namespace TransportationManagement.Services.Impl
{
    public class YBSCompanyServiceImpl : AbstractServiceImpl<YBSCompany>, YBSCompanyService
    {
        private readonly ILogger<YBSCompanyServiceImpl> _logger;

        public YBSCompanyServiceImpl(HumanResourceManagementDBContext context, ILogger<YBSCompanyServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<YBSCompany> GetAllYBSCompanys()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetAllYBSCompanys] Get YBSCompany list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get YBSCompany list. <<<<<<<<<<");
                return GetAll().Where(ybsCompany => !ybsCompany.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving YBSCompany list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SelectListItem> GetSelectListYBSCompanys()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetSelectListYBSCompanys] Get SelectList YBSCompany selectbox's options and values. <<<<<<<<<<");
            try
            {
                var lstYBSCompanys = new List<SelectListItem>();
                List<YBSCompany> YBSCompanys = GetUniqueYBSCompanys();
                _logger.LogInformation($">>>>>>>>>> Make YBSCompany selectbox's options and values. <<<<<<<<<<");
                try
                {
                    lstYBSCompanys = YBSCompanys.Select(ybsCompany => new SelectListItem()
                    {
                        Value = ybsCompany.YBSCompanyPkid.ToString(),
                        Text = ybsCompany.YBSCompanyName
                    }).ToList();
                    _logger.LogInformation($">>>>>>>>>> Success. Make YBSCompany selectbox's options and values. <<<<<<<<<<");

                }
                catch (Exception e)
                {
                    _logger.LogInformation($">>>>>>>>>> Error occur. Make YBSCompany selectbox's options and values. <<<<<<<<<<");
                    throw;
                }
                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "-----ရွေးချယ်ရန်-----"
                };

                lstYBSCompanys.Insert(0, defItem);
                _logger.LogInformation($">>>>>>>>>> Success. Make YBSCompany selectbox's options and values. <<<<<<<<<<");
                return lstYBSCompanys;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. Get SelectList YBSCompany selectbox's options and values. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<YBSCompany> GetAllYBSCompanysWithPagin(int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetAllYBSCompanysWithPagin] Get YBSCompany pagination list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get YBSCompany pagination list. <<<<<<<<<<");
                return GetAllWithPagin(GetAllYBSCompanys(), pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YBSCompany pagination list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSCompany> GetUniqueYBSCompanys()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][GetUniqueYBSCompanys] Get unique YBSCompany list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique YBSCompany list. <<<<<<<<<<");
                return GetUniqueList(yBSCompany => yBSCompany.YBSCompanyPkid).Where(yBSCompany => !yBSCompany.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique YBSCompany list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSCompany FindYBSCompanyById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][FindYBSCompanyById] Find YBSCompany by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSCompany by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSCompany by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateYBSCompany(YBSCompany yBSCompany)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][CreateYBSCompany] Create YBSCompany. <<<<<<<<<<");
            try
            {
                yBSCompany.IsDeleted = false;
                _logger.LogInformation($">>>>>>>>>> Success. Create YBSCompany. <<<<<<<<<<");
                return Create(yBSCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating YBSCompany. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool EditYBSCompany(YBSCompany yBSCompany)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][EditYBSCompany] Edit YBSCompany. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit YBSCompany. <<<<<<<<<<");
                return Update(yBSCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating YBSCompany. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteYBSCompany(YBSCompany yBSCompany)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSCompanyServiceImpl][DeleteYBSCompany] Soft delete YBSCompany. <<<<<<<<<<");
            try
            {
                yBSCompany.IsDeleted = true;
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete YBSCompany. <<<<<<<<<<");
                return Update(yBSCompany);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting YBSCompany. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

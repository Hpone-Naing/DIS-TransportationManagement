using TransportationManagement.Data;
using TransportationManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace TransportationManagement.Services.Impl
{
    public class YBSTypeServiceImpl : AbstractServiceImpl<YBSType>, YBSTypeService
    {
        private readonly ILogger<YBSTypeServiceImpl> _logger;

        private readonly YBSCompanyService _ybsCompanyService;
        public YBSTypeServiceImpl(HumanResourceManagementDBContext context, ILogger<YBSTypeServiceImpl> logger, YBSCompanyService ybsCompanyService) : base(context, logger)
        {
            _logger = logger;
            _ybsCompanyService = ybsCompanyService;
        }

        public List<YBSType> GetAllYBSTypes()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetAllYBSTypes] Get YBSType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get YBSType list. <<<<<<<<<<");
                return GetAll().Where(ybsType => !ybsType.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving YBSType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSType> GetUniqueYBSTypes(int ybsCompanyPkId = 0)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetUniqueYBSTypes] Get unique YBSType list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get unique YBSType list. <<<<<<<<<<");
                List<YBSType> ybsTypes;
                if (ybsCompanyPkId > 0)
                {
                    ybsTypes = _context.YBSTypes
                                         .Where(ybsType => !ybsType.IsDeleted && ybsType.YBSCompanyPkid == ybsCompanyPkId)
                                         .GroupBy(ybsType => ybsType.YBSTypePkid)
                                         .Select(g => g.First())
                                         .ToList();
                }
                else
                {
                    ybsTypes = _context.YBSTypes
                                             .Where(ybsType => !ybsType.IsDeleted)
                                             .GroupBy(ybsType => ybsType.YBSTypePkid)
                                             .Select(g => g.First())
                                             .ToList();
                }

                var vehicleDataCount = _context.VehicleDatas
                    .GroupBy(vehicle => vehicle.VehicleTypePkid)
                    .Select(group => new { VehicleTypePkid = group.Key, Count = group.Count() })
                    .ToList();

                foreach (var ybsType in ybsTypes)
                {
                    var ybsTypeCount = vehicleDataCount.FirstOrDefault(x => x.VehicleTypePkid == ybsType.YBSTypePkid);
                    ybsType.TotalYBSNumber = ybsTypeCount != null ? ybsTypeCount.Count : 0;
                }

                return ybsTypes;
                //return GetUniqueList(ybsType => ybsType.YBSTypePkid).Where(ybsType => !ybsType.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing unique YBSType list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSType> GetUniqueYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            Console.WriteLine("here GetUniqueYBSTypesByYBSCompanyId...............................................");
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetUniqueYBSTypesByYBSCompanyId] Find YBSType by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSType by pkId. <<<<<<<<<<");

                return GetUniqueYBSTypes(ybsCompanyId);//GetListByIntVal("YBSCompanyPkid", ybsCompanyId).Where(ybsType => !ybsType.IsDeleted).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSType by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<YBSType> GetAllYBSTypesWithPagin(int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetAllYBSTypesWithPagin] Get YBSType pagination list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get YBSType pagination list. <<<<<<<<<<");
                return GetAllWithPagin(GetAllYBSTypes(), pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YBSType pagination list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SelectListItem> GetSelectListYBSTypesByYBSCompanyId(int ybsCompanyId = 1)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][GetSelectListYBSTypesByYBSCompanyId] Get SelectList YBSType selectbox's options and values. <<<<<<<<<<");
            try
            {
                var lstYBSTypes = new List<SelectListItem>();
                _logger.LogInformation($">>>>>>>>>> Make YBSType selectbox's options and values. <<<<<<<<<<");
                try
                {
                    lstYBSTypes = GetUniqueYBSTypesByYBSCompanyId(ybsCompanyId)
                .Select(
                    ybsType => new SelectListItem
                    {
                        Value = ybsType.YBSTypePkid.ToString(),
                        Text = ybsType.YBSTypeName + ";" + ybsType.TotalYBSNumber,                        
                       
                    }).ToList();
                }
                catch (Exception e)
                {
                    _logger.LogInformation($">>>>>>>>>> Error occur. Make YBSType selectbox's options and values. <<<<<<<<<<");
                    throw;
                }
                var defItem = new SelectListItem()
                {
                    Value = "",
                    Text = "-----ရွေးချယ်ရန်-----"
                };

                lstYBSTypes.Insert(0, defItem);
                return lstYBSTypes;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. Get SelectList YBSType selectbox's options and values. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSType FindYBSTypeById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][FindYBSTypeById] Find YBSType by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSType by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSType by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSType FindYBSTypeByName(string name)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][FindYBSTypeByName] Find YBSType by name. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSType by name. <<<<<<<<<<");
                return FindByString("YBSTypeName", name) ;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSType by name. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSType FindYBSTypeByNameAndCompany(string name, int companyPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][FindYBSTypeByNameAndCompany] Find YBSType by name and company. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find YBSType by name and company. <<<<<<<<<<");
                return _context.YBSTypes.FirstOrDefault(ybsType => ybsType.YBSCompanyPkid == companyPkId && ybsType.YBSTypeName == name);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSType by name and company. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateYBSType(int ybsCompanyPkId, YBSType yBSType)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][CreateYBSType] Create YBSType. <<<<<<<<<<");
            try
            {
                YBSCompany ybsCompany = _ybsCompanyService.FindYBSCompanyById(ybsCompanyPkId);
                yBSType.YBSCompany = ybsCompany;
                yBSType.IsDeleted = false;
                _logger.LogInformation($">>>>>>>>>> Success. Create YBSType. <<<<<<<<<<");

                return Create(yBSType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating YBSType. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateYBSType(YBSType yBSType)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][CreateYBSType] Create YBSType. <<<<<<<<<<");
            try
            {
                yBSType.IsDeleted = false;
                _logger.LogInformation($">>>>>>>>>> Success. Create YBSType. <<<<<<<<<<");
                return Create(yBSType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating YBSType. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool EditYBSType(int ybsCompanyPkId, YBSType yBSType)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][EditYBSType] Edit YBSType. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit YBSType. <<<<<<<<<<");
                YBSCompany ybsCompany = _ybsCompanyService.FindYBSCompanyById(ybsCompanyPkId);
                yBSType.YBSCompany = ybsCompany;
                return Update(yBSType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating YBSType. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteYBSType(YBSType yBSType)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSTypeServiceImpl][DeleteYBSType] Soft delete YBSType. <<<<<<<<<<");
            try
            {
                yBSType.IsDeleted = true;
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete YBSType. <<<<<<<<<<");
                return Update(yBSType);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting YBSType. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

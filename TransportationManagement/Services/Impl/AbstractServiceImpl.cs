using TransportationManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransportationManagement.Paging;
using TransportationManagement.Classes;

namespace TransportationManagement.Services.Impl
{
    public class AbstractServiceImpl<T> : AbstractService<T> where T : class
    {
        private readonly ILogger<AbstractServiceImpl<T>> _logger;
        protected readonly HumanResourceManagementDBContext _context;

        public AbstractServiceImpl(HumanResourceManagementDBContext context, ILogger<AbstractServiceImpl<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public PagingList<T> GetAllWithPagin(List<T> list, int? pageNo, int pageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][GetAllWithPagin] Retrieve Object List and make pagination <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve Object List success. <<<<<<<<<<");
                return PagingList<T>.CreateAsync(list.AsQueryable<T>(), pageNo ?? 1, pageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing Object List. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<T> GetUniqueList(Func<T, object> keySelector)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][GetUniqueList] Retrieve Unique List <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Retrieve Unique List success. <<<<<<<<<<");
                return _context.Set<T>().GroupBy(keySelector).Select(group => group.First()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing Unique Object List. <<<<<<<<<<" + e);
                throw;
            }
        }

        protected bool IsSearchDataContained(object obj, string searchData)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][IsSearchDataContained]Check searchString contain in table columns value <<<<<<<<<<");
            try
            {
                if (obj != null)
                {
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            string propValue = (string)prop.GetValue(obj);

                            if (propValue != null)
                            {

                                if (propValue.Trim().IndexOf(searchData, StringComparison.OrdinalIgnoreCase) >= 0)
                                {

                                    _logger.LogInformation(">>>>>>>>>> Match searchString and colVal <<<<<<<<<<");
                                    return true;
                                }
                            }

                        }
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Not match searchString and colVal <<<<<<<<<<");
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when checking searchString contain in table columns value. <<<<<<<<<<" + e);
                throw;
            }
        }
        private IQueryable<T> FiltersWithoutAdvSearchOptions<T>(IQueryable<T> query, AdvanceSearch advanceSearch) where T : class
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][FiltersWithoutAdvSearchOptions] Check advanceSearch values contain in table columns value <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Check advanceSearch values contain in table columns value. <<<<<<<<<<");
                return query.Where(obj =>
                (!string.IsNullOrEmpty(advanceSearch.POSInstalled) && EF.Property<string>(obj, "POSInstalled") != null && EF.Property<string>(obj, "POSInstalled").ToLower().Contains(advanceSearch.POSInstalled.ToLower()))
                || (!string.IsNullOrEmpty(advanceSearch.CctvInstalled) && EF.Property<string>(obj, "CctvInstalled") != null && EF.Property<string>(obj, "CctvInstalled").ToLower().Contains(advanceSearch.CctvInstalled.ToLower()))
                || (!string.IsNullOrEmpty(advanceSearch.TelematicDeviceInstalled) && EF.Property<string>(obj, "TelematicDeviceInstalled") != null && EF.Property<string>(obj, "TelematicDeviceInstalled").ToLower().Contains(advanceSearch.TelematicDeviceInstalled.ToLower()))
                );
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when checking advanceSearch value contain in table columns value. <<<<<<<<<<" + e);
                throw;
            }
        }
        public bool FilterAdvSearchOptions(Object obj, string colName, string advSearchString, string advSearchOption)
        {

            string columnValue = (string)EF.Property<string>(obj, colName);
            if (int.TryParse(columnValue, out int parseIntColumnValue))
            {
                var parseIntAdvSeaerchString = int.Parse(advSearchString);

                switch (advSearchOption)
                {
                    case ">":
                        return parseIntColumnValue > parseIntAdvSeaerchString;
                    case ">=":
                        return parseIntColumnValue >= parseIntAdvSeaerchString;
                    case "<":
                        return parseIntColumnValue < parseIntAdvSeaerchString;
                    case "<=":
                        return parseIntColumnValue <= parseIntAdvSeaerchString;
                    default:
                        return parseIntColumnValue == parseIntAdvSeaerchString; ; //throw new ArgumentException($"Invalid option: {option}");
                }
            }
            else
            {
                return false;
            }
        }

        public List<T> AdvanceSearch<T>(AdvanceSearch advanceSearch, DbSet<T> dbSet) where T : class
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][AdvanceSearch] AdvanceSearch <<<<<<<<<<");
            try
            {
                var query = dbSet.AsQueryable();
                query = FiltersWithoutAdvSearchOptions(query, advanceSearch);
                _logger.LogInformation($">>>>>>>>>> Success AdvanceSearch. <<<<<<<<<<");
                return query.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur in AdvanceSearch. <<<<<<<<<<" + e);
                throw;
            }
        }

        public T FindById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][IsSearchDataContained] Find object by pkId <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Found match object by pkId <<<<<<<<<<");
                return _context.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding object by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public T FindByString(string columnName, string str)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][FindByString] Find object's specific columnName's value that match stringValue <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Found object's specific columnName's value match stringValue <<<<<<<<<<");
                return _context.Set<T>().FirstOrDefault(entity =>
                EF.Property<string>(entity, columnName) == str);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding object's specific columnName's value match stringValue. <<<<<<<<<<" + e);
                throw;
            }
        }

        public T FindByIntVal(string columnName, int intVal)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][FindByIntVal] Find object's specific columnName's value that match intValue <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Found object's specific columnName's value match intValue <<<<<<<<<<");
                return _context.Set<T>().FirstOrDefault(entity =>
                EF.Property<int>(entity, columnName) == intVal);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding object's specific columnName's value match intValue. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<T> GetListByIntVal(string columnName, int intVal)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][GetListByIntVal] Get object list match specific columnName's value that match intValue <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Get object list match specific columnName's value match intValue <<<<<<<<<<");
                return _context.Set<T>().Where(entity =>
                EF.Property<int>(entity, columnName) == intVal).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting object list match specific columnName's value match intValue. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool Create(T t)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][Create] Save object.  <<<<<<<<<<");
            try
            {
                _context.Set<T>().Add(t);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Create Fail. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool Update(T t)
        {
            _logger.LogInformation(">>>>>>>>>> [AbstractServiceImpl][Update] Update object.  <<<<<<<<<<");
            try
            {
                _context.Entry(t).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Update Fail. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

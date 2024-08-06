using TransportationManagement.Classes;
using TransportationManagement.Data;
using TransportationManagement.Paging;
using Microsoft.EntityFrameworkCore;
using TransportationManagement.Models;
using System.Data;

namespace TransportationManagement.Services.Impl
{
    public class VehicleDataServiceImpl : AbstractServiceImpl<VehicleData>, VehicleDataService
    {
        private readonly ILogger<VehicleDataServiceImpl> _logger;

        public VehicleDataServiceImpl(HumanResourceManagementDBContext context, ILogger<VehicleDataServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<VehicleData> GetAllVehicles()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicles] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return GetAll().Where(vehicle => vehicle.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<VehicleData> GetAllVehiclesEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicles] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicleData => vehicleData.IsDeleted == false)
                    .Include(ybsCompany => ybsCompany.YBSCompany)
                    .Include(ybsType => ybsType.YBSType)
                    .Include(fuelType => fuelType.FuelType)
                    .Include(manufacturer => manufacturer.Manufacturer).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        private List<VehicleData> GetAllVehicleWithLazyLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicleWithLazyLoad] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicleData => vehicleData.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        } 

        private List<VehicleData> GetAllVehicleWithYBSCompany()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicleWithYBSCompany] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicleData => vehicleData.IsDeleted == false)
                    .Include(ybsCompany => ybsCompany.YBSCompany).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        private List<VehicleData> GetAllVehicleWithYBSType()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicleWithYBSType] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicleData => vehicleData.IsDeleted == false)
                    .Include(ybsType => ybsType.YBSType).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        private List<VehicleData> GetAllVehicleWithManufacturer()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicleWithManufacturer] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicleData => vehicleData.IsDeleted == false)
                    .Include(manufacturer => manufacturer.Manufacturer).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        private List<VehicleData> GetAllVehicleWithFuelType()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicleWithFuelType] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicleData => vehicleData.IsDeleted == false)
                    .Include(fuelType => fuelType.FuelType).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        private List<VehicleData> searchResultList(string searchString)
        {
            List<VehicleData> resultList = GetAllVehicleWithLazyLoad().Where(vehicle => IsSearchDataContained(vehicle, searchString)).AsQueryable().ToList();
            if (resultList.Count < 1)
            {
                Console.WriteLine("here 1.............................");
                resultList = GetAllVehicleWithYBSCompany().Where(vehicle => IsSearchDataContained(vehicle.YBSCompany, searchString)).AsQueryable().ToList();
            }
            if (resultList.Count < 1)
            {
                Console.WriteLine("here 2.............................");

                resultList = GetAllVehicleWithYBSType().Where(vehicle => IsSearchDataContained(vehicle.YBSType, searchString)).AsQueryable().ToList();
            }
            if (resultList.Count < 1)
            {
                Console.WriteLine("here 3.............................");

                resultList = GetAllVehicleWithFuelType().Where(vehicle => IsSearchDataContained(vehicle.FuelType, searchString)).AsQueryable().ToList();
            }
            if (resultList.Count < 1)
            {
                Console.WriteLine("here 4.............................");

                resultList = GetAllVehicleWithManufacturer().Where(vehicle => IsSearchDataContained(vehicle.Manufacturer, searchString)).AsQueryable().ToList();
            }
            Console.WriteLine("here 5.............................");

            return resultList;
        }

        public PagingList<VehicleData> GetAllVehiclesWithPagin(string searchWord, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string? searchOption = "")
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehiclesWithPagin] SearchAll or AdvanceSearch or GetAll VehicleData paginate eager load list. <<<<<<<<<<");

            try
            {
                IQueryable<VehicleData> query = _context.VehicleDatas
                    .Where(vehicleData => vehicleData.IsDeleted == false);

                if (!string.IsNullOrEmpty(searchWord))
                {
                    string searchString = searchWord.Trim();
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result VehicleData paginate eager load list. <<<<<<<<<<");
                    if (!string.IsNullOrEmpty(searchOption))
                    {
                        Console.WriteLine("here search option not null........................");
                        query = query.Where(vehicle => vehicle.YBSType.YBSTypeName == searchString);
                    }
                    else
                    {
                        Console.WriteLine("here search option null........................");

                        query = query.Where(vehicle =>
                        EF.Functions.Like(vehicle.VehicleNumber ?? "", $"%{searchString}%") ||
                        EF.Functions.Like(vehicle.YBSName ?? "", $"%{searchString}%") ||
                        EF.Functions.Like(vehicle.ManufacturedYear ?? "", $"%{searchString}%") ||
                        EF.Functions.Like(vehicle.CngQty ?? "", $"%{searchString}%") ||
                        EF.Functions.Like(vehicle.ManufacturedYear ?? "", $"%{searchString}%") ||
                        EF.Functions.Like(vehicle.OperatorName ?? "", $"%{searchString}%") ||
                        EF.Functions.Like(vehicle.RegistrantOperatorName ?? "", $"%{searchString}%") ||

                            EF.Functions.Like(vehicle.YBSCompany.YBSCompanyName, $"%{searchString}%") ||
                            EF.Functions.Like(vehicle.YBSType.YBSTypeName, $"%{searchString}%") ||
                            EF.Functions.Like(vehicle.FuelType.FuelTypeName, $"%{searchString}%") ||
                            EF.Functions.Like(vehicle.Manufacturer.ManufacturerName, $"%{searchString}%"));
                    }
                }

                else if (!string.IsNullOrEmpty(advanceSearch.POSInstalled) ||
                         !string.IsNullOrEmpty(advanceSearch.TelematicDeviceInstalled) ||
                         !string.IsNullOrEmpty(advanceSearch.CctvInstalled) ||
                         !string.IsNullOrEmpty(advanceSearch.YBSCompany) ||
                         !string.IsNullOrEmpty(advanceSearch.Manufacturer) ||
                         !string.IsNullOrEmpty(advanceSearch.YBSName) ||
                         !string.IsNullOrEmpty(advanceSearch.FuelType)
                         )
                {
                    _logger.LogInformation($">>>>>>>>>> Get AdvanceSearch result VehicleData paginate eager load list. <<<<<<<<<<");
                    query = query.Include(vehicle => vehicle.FuelType).Where(vehicleData =>
                (string.IsNullOrEmpty(advanceSearch.POSInstalled) || EF.Property<string>(vehicleData, "POSInstalled") != null && EF.Property<string>(vehicleData, "POSInstalled").ToLower().Contains(advanceSearch.POSInstalled.ToLower())) &&
                (string.IsNullOrEmpty(advanceSearch.CctvInstalled) || EF.Property<string>(vehicleData, "CctvInstalled") != null && EF.Property<string>(vehicleData, "CctvInstalled").ToLower().Contains(advanceSearch.CctvInstalled.ToLower())) &&
                (string.IsNullOrEmpty(advanceSearch.TelematicDeviceInstalled) || EF.Property<string>(vehicleData, "TelematicDeviceInstalled") != null && EF.Property<string>(vehicleData, "TelematicDeviceInstalled").ToLower().Contains(advanceSearch.TelematicDeviceInstalled.ToLower())) &&
                (string.IsNullOrEmpty(advanceSearch.YBSCompany) || EF.Property<string>(vehicleData.YBSCompany, "YBSCompanyName") != null && EF.Property<string>(vehicleData.YBSCompany, "YBSCompanyName").ToLower().Contains(advanceSearch.YBSCompany.ToLower())) &&
                (string.IsNullOrEmpty(advanceSearch.Manufacturer) || EF.Property<string>(vehicleData.Manufacturer, "ManufacturerName") != null && EF.Property<string>(vehicleData.Manufacturer, "ManufacturerName").ToLower().Contains(advanceSearch.Manufacturer.ToLower())) &&
                (string.IsNullOrEmpty(advanceSearch.YBSName) || EF.Property<string>(vehicleData, "YBSName") != null && EF.Property<string>(vehicleData, "YBSName").ToLower().Contains(advanceSearch.YBSName.ToLower())) &&
                (string.IsNullOrEmpty(advanceSearch.FuelType) || EF.Property<string>(vehicleData.FuelType, "FuelTypeName") != null && EF.Property<string>(vehicleData.FuelType, "FuelTypeName").ToLower().Contains(advanceSearch.FuelType.ToLower())));


                }

                Console.WriteLine("page no....................." + pageNo);
                int currentPage = (pageNo.HasValue && pageNo.Value > 0) ? pageNo.Value : 1;
                Console.WriteLine("cur page....................." + currentPage);

                int skip = (currentPage - 1) * PageSize;
                Console.WriteLine("skip....................." + skip);

                int totalRecords = query.Count();
                Console.WriteLine("totalrecords....................." + totalRecords);

                
                var resultList = query
                    .Skip(skip)
                    .Take(PageSize)
                    .Include(vehicle => vehicle.YBSCompany)
                    .Include(vehicle => vehicle.YBSType)
                    .Include(vehicle => vehicle.FuelType)
                    .Include(vehicle => vehicle.Manufacturer)
                    .ToList();

                _logger.LogInformation($">>>>>>>>>> Success. SearchAll or GetAll VehicleData paginate eager load list. <<<<<<<<<<");

                PagingList<VehicleData> paginatedList = new PagingList<VehicleData>(resultList, totalRecords, pageNo ?? 0, PageSize);

                return paginatedList;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or AdvanceSearch or GetAll VehicleData paginate eager load list. <<<<<<<<<<" + e);
                throw;
            }
        }


        public PagingList<VehicleData> GetAllVehiclesWithPaginForExcelExport(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehiclesWithPagin] SearchAll or AdvanceSearch or GetAll VehicleData paginate eger load list. <<<<<<<<<<");
            try
            {
                List<VehicleData> vehicleDatas = GetAllVehiclesEgerLoad();
                List<VehicleData> resultList = new List<VehicleData>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                        /*resultList = GetAllVehiclesEgerLoad()
                            .Where(vehicle => IsSearchDataContained(vehicle, searchString))
                            .Where(vehicle => IsSearchDataContained(vehicle.YBSCompany, searchString))
                            .Where(vehicle => IsSearchDataContained(vehicle.YBSType, searchString))
                            .Where(vehicle => IsSearchDataContained(vehicle.FuelType, searchString))
                            .Where(vehicle => IsSearchDataContained(vehicle.Manufacturer, searchString))
                            .AsQueryable()
                            .ToList();*/
                        Console.WriteLine(" excel searchString......." + searchString);
                        resultList = searchResultList(searchString);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else if (!string.IsNullOrEmpty(advanceSearch.POSInstalled) || !string.IsNullOrEmpty(advanceSearch.TelematicDeviceInstalled) || !string.IsNullOrEmpty(advanceSearch.CctvInstalled))
                {
                    Console.WriteLine("Here adv search not null");
                    _logger.LogInformation($">>>>>>>>>> Get AdvanceSearch result VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get AdvanceSearch result VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = AdvanceSearch(advanceSearch, _context.VehicleDatas).Where(vehicleData => vehicleData.IsDeleted == false).ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get AdvanceSearch result VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = vehicleDatas;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> Success. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or AdvanceSearch or GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][CreateVehicle] Create VehicleData. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Create VehicleData. <<<<<<<<<<");
                vehicleData.IsDeleted = false;
                vehicleData.CreatedDate = DateTime.Now;
                vehicleData.RegistrationDate = DateTime.Now;
                return Create(vehicleData);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating VehicleData. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][EditVehicle] Edit VehicleData. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit VehicleData. <<<<<<<<<<");
                vehicleData.IsDeleted = false;
                vehicleData.CreatedDate = DateTime.Now;
                vehicleData.RegistrationDate = DateTime.Now;
                return Update(vehicleData);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating VehicleData. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][DeleteVehicle] Soft delete VehicleData. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Soft delete VehicleData. <<<<<<<<<<");
                vehicleData.IsDeleted = true;
                return Update(vehicleData);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting VehicleData. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool HardDeleteVehicle(VehicleData vehicleData)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][DeleteVehicle] Hard delete VehicleData. <<<<<<<<<<");
            try
            {
                var entity = _context.VehicleDatas.Find(vehicleData.VehicleDataPkid);
                if (entity == null)
                {
                    _logger.LogWarning(">>>>>>>>>> VehicleData not found. <<<<<<<<<<");
                    return false;
                }

                _context.VehicleDatas.Remove(entity);
                _context.SaveChanges();
                _logger.LogInformation($">>>>>>>>>> Success. Hard deleted VehicleData with ID {vehicleData.VehicleDataPkid}. <<<<<<<<<<");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occurred when hard deleting VehicleData. <<<<<<<<<<" + e);
                return false;
            }
        }


        public VehicleData FindVehicleDataById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataById] Find VehicleData by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataByVehicleNumber(string vehicleNumber)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByVehicleNumber] Find VehicleData by vehicleNumber. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by vehicleNumber. <<<<<<<<<<");
                return FindByString("VehicleNumber", vehicleNumber);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by vehicleNumber. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByIdEgerLoad] Find VehicleData by pkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId with eger load. <<<<<<<<<<");
                return _context.VehicleDatas
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .Include(vehicle => vehicle.Manufacturer)
                           .Include(vehicle => vehicle.FuelType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeVehicleDataExcelData(PagingList<VehicleData> vehicleDatas, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][MakeVehicleDataExcelData] Assign SearchAll or GetAll VehicleData list to dataTable. <<<<<<<<<<");
            DataTable dt = new DataTable("Call Center တိုင်ကြားမှုစာရင်း");
            dt.Columns.AddRange(new DataColumn[14] {
                                        new DataColumn("စဥ်"),
                                        new DataColumn("ကုမ္ပဏီအမည်"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("YBSယာဥ်လိုင်း"),
                                        new DataColumn("ယာဥ်အမျိုးအမည်"),
                                        new DataColumn("ယာဥ်အမျိုးအစား"),
                                        new DataColumn("ထုတ်လုပ်သည့်ခုနှစ်"),
                                        new DataColumn("စက်သုံးဆီအမျိုးအစား(EV/CNG/Diesel)"),
                                        new DataColumn("CNGအိုးသက်တမ်း"),
                                        new DataColumn("Operatorအမည်"),
                                        new DataColumn("ကမ-၃အမည်ပေါက်"),
                                        new DataColumn("Telematics"),
                                        new DataColumn("CCTV"),
                                        new DataColumn("YPS(POS)"),
                                        });
            var list = new List<VehicleData>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all VehicleData eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all VehicleData eger load list. <<<<<<<<<<");
                    list = GetAllVehiclesEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all VehicleData eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult VehicleData list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult VehicleData eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult VehicleData eger load list. <<<<<<<<<<");
                    list = vehicleDatas;
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult VehicleData eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var vehicleData in list)
                    {
                        dt.Rows.Add(vehicleData.SerialNo, vehicleData.YBSCompany.YBSCompanyName, vehicleData.VehicleNumber, vehicleData.YBSType.YBSTypeName, vehicleData.YBSName, vehicleData.Manufacturer.ManufacturerName, vehicleData.ManufacturedYear, vehicleData.FuelType.FuelTypeName, vehicleData.CngQty, vehicleData.OperatorName, vehicleData.RegistrantOperatorName, (vehicleData.TelematicDeviceInstalled != "yes" && vehicleData.TelematicDeviceInstalled != "no") ? vehicleData.TelematicDeviceInstalled : vehicleData.TelematicDeviceInstalled == "yes" ? "ü" : "û", (vehicleData.CctvInstalled != "yes" && vehicleData.CctvInstalled != "no") ? vehicleData.CctvInstalled : vehicleData.CctvInstalled == "yes" ? "ü" : "û", (vehicleData.POSInstalled != "yes" && vehicleData.POSInstalled != "no") ? vehicleData.POSInstalled : vehicleData.POSInstalled == "yes" ? "ü" : "û");
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll VehicleData list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}

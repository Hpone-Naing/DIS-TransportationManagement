using System.Data;
using TransportationManagement.Models;
using TransportationManagement.Paging;

namespace TransportationManagement.Services
{
    public interface YboRecordService
    {
        bool CreateYboRecord(YboRecord yboRecord);
        List<YboRecord> GetAllYboRecords();
        PagingList<YboRecord> GetAllYboRecordsWithPagin(string searchString, int? pageNo, int PageSize);
        bool DeleteYboRecord(YboRecord yboRecord);
        YboRecord FindYboRecordById(int id);
        YboRecord FindYboRecordByIdEgerLoad(int id);
        bool EditYboRecord(YboRecord yboRecord);
        DataTable MakeYboRecordExcelData(PagingList<YboRecord> yboRecords, bool exportAll);
    }
}

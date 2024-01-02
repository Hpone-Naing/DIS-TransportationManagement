using TransportationManagement.Paging;

namespace TransportationManagement.Services
{
    public interface AbstractService<T>
    {
        public List<T> GetAll();
        public PagingList<T> GetAllWithPagin(List<T> list, int? pageNo, int pageSize);
        public List<T> GetUniqueList(Func<T, object> keySelector);
        public T FindById(int id);
        public T FindByString(string columnName, string str);
        public List<T> GetListByIntVal(string columnName, int intVal);
        public bool Create(T entity);
        public bool Update(T t);

    }
}

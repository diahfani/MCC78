using WebAPI.Model;

namespace WebAPI.Contracts;

public interface IGenericRepository<T>
{
    T Create(T account);
    bool Update(T account);
    bool Delete(Guid guid);
    IEnumerable<T> GetAll();
    T? GetByGuid(Guid guid);
}

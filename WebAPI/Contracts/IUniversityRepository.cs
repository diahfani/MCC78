using WebAPI.Model;

namespace WebAPI.Contracts;

public interface IUniversityRepository : IGenericRepository<University>
{
    IEnumerable<University>? GetByName(string name);
}

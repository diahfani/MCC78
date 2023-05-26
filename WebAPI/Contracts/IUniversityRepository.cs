using WebAPI.Model;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Contracts;

public interface IUniversityRepository : IGenericRepository<University>
{
    UniversityVM? GetByName(string name);
    UniversityVM CreateWithValidate(University university);

}

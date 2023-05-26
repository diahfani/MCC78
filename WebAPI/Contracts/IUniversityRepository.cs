using WebAPI.Model;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Contracts;

public interface IUniversityRepository : IGenericRepository<University>
{
    /*IEnumerable<UniversityVM>? GetByName(string name);*/
    University CreateWithValidate(University university);

}

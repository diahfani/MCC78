using WebAPI.Model;
using WebAPI.ViewModels.Educations;

namespace WebAPI.Contracts;

public interface IEducationRepository : IGenericRepository<Education>
{
    IEnumerable<Education> GetByUniversityGuid(Guid universityGuid);
}


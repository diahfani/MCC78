using WebAPI.Model;

namespace WebAPI.Contracts;

public interface IEducationRepository : IGenericRepository<Education>
{
    IEnumerable<Education> GetByUniversityGuid(Guid universityGuid);
}


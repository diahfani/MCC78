using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class EducationRepository : GenericRepository<Education>, IEducationRepository 
{
    public EducationRepository(BookingRoomsDBContext context) : base(context)
    {
    }

    public IEnumerable<Education> GetByUniversityGuid(Guid universityGuid)
    {
        return _context.Set<Education>().Where(e => e.UniversityGuid == universityGuid);
    }
}

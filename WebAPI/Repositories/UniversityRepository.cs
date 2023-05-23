using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
    // panggil context dari bookingroomdbcontext
    public UniversityRepository(BookingRoomsDBContext context) : base(context)
    { 
    }
    
    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>().Where(u => u.Name.Contains(name));
    }

}

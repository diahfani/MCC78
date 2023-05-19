using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class UniversityRepository : IUniversityRepository
{
    // panggil context dari bookingroomdbcontext
    private readonly BookingRoomsDBContext _context;
    public UniversityRepository(BookingRoomsDBContext context)
    {
        _context = context;
    }
    
    public University Create(University university)
    {
        try
        {
            // add itu method dari linq
            _context.Set<University>().Add(university);
            _context.SaveChanges();
            return university;
        } catch
        {
            return new University();
        }
    }

    public bool Delete(Guid guid)
    {
        try
        {
            var university = GetByGuid(guid);
            if (university == null)
            {
                return false;
            }
            _context.Set<University>().Remove(university);
            _context.SaveChanges();
            return true;
        } catch
        {
            return false;
        }
    }

    public IEnumerable<University> GetAll()
    {
        return _context.Set<University>().ToList();
    }

    // question mark itu untuk buat exception kalo si guid yg dicari itu gaada
    public University? GetByGuid(Guid guid)
    {
        return _context.Set<University>().Find(guid);
    }

    public bool Update(University university)
    {
        try
        {
            _context.Set<University>().Update(university);
            _context.SaveChanges();
            return true;
        } catch
        {
            return false;
        }
    }
}

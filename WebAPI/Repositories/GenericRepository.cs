using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BookingRoomsDBContext _context;
    public GenericRepository(BookingRoomsDBContext context)
    {
        _context = context;
    }

    public T Create(T item)
    {
        try
        {
            // add itu method dari linq
            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }
        catch
        {
            return default(T);
        }
    }

    public bool Delete(Guid guid)
    {
        try
        {
            var item = GetByGuid(guid);
            if (item == null)
            {
                return false;
            }
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetByGuid(Guid guid)
    {
        return _context.Set<T>().Find(guid);
    }

    public bool Update(T item)
    {
        try
        {
            _context.Set<T>().Update(item);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

}

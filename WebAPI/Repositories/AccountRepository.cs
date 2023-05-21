using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class AccountRepository : IGenericRepository<Account>
{
    private readonly BookingRoomsDBContext _context;
    public AccountRepository(BookingRoomsDBContext context)
    {
        _context = context;
    }

    public Account Create(Account account)
    {
        try
        {
            // add itu method dari linq
            _context.Set<Account>().Add(account);
            _context.SaveChanges();
            return account;
        }
        catch
        {
            return new Account();
        }
    }

    public bool Delete(Guid guid)
    {
        try
        {
            var account = GetByGuid(guid);
            if (account  == null)
            {
                return false;
            }
            _context.Set<Account>().Remove(account);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Set<Account>().ToList();
    }

    public Account? GetByGuid(Guid guid)
    {
        return _context.Set<Account>().Find(guid);
    }

    public bool Update(Account account)
    {
        try
        {
            _context.Set<Account>().Update(account);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(BookingRoomsDBContext context) : base(context)
    {
    }


    public IEnumerable<Employee> GetByEmail(string email)
    {
        return _context.Set<Employee>().Where(e => e.Email.Contains(email));
    }
}

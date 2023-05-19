using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly BookingRoomsDBContext _context;
    public EmployeeRepository(BookingRoomsDBContext context)
    {
        _context = context;
    }

    public Employee Create(Employee employee)
    {
        try
        {
            // add itu method dari linq
            _context.Set<Employee>().Add(employee);
            _context.SaveChanges();
            return employee;
        }
        catch
        {
            return new Employee();
        }
    }

    public bool Delete(Guid guid)
    {
        try
        {
            var employee = GetByGuid(guid);
            if (employee == null)
            {
                return false;
            }
            _context.Set<Employee>().Remove(employee);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<Employee> GetAll()
    {
        return _context.Set<Employee>().ToList();
    }

    public Employee? GetByGuid(Guid guid)
    {
        return _context.Set<Employee>().Find(guid);
    }

    public bool Update(Employee employee)
    {
        try
        {
            _context.Set<Employee>().Update(employee);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

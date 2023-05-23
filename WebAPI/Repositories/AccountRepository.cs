using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;

namespace WebAPI.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    private readonly IEmployeeRepository _employeeRepository;
    public AccountRepository(BookingRoomsDBContext context, IEmployeeRepository employeeRepository) : base(context)
    {
        _employeeRepository = employeeRepository;
    }
    public IEnumerable<Account> GetByEmployeeGuid(Guid employeeGuid)
    {
        return _context.Set<Account>().Where(a => a.Employee.Guid == employeeGuid);
    }

    public AccountEmployeeVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _employeeRepository.GetAll();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new AccountEmployeeVM
                    {
                        Email = emp.Email,
                        Password = acc.Password
                    };

        return query.FirstOrDefault();
    }
}

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

    public LoginVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _employeeRepository.GetAll();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new LoginVM
                    {
                        Email = emp.Email,
                        Password = acc.Password
                    };

        return query.FirstOrDefault();
    }
}

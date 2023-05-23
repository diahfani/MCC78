using WebAPI.Model;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;

namespace WebAPI.Contracts;

public interface IAccountRepository : IGenericRepository<Account>
{
    /*IEnumerable<Account> GetByEmployeeGuid(Guid employeeGuid);*/
    AccountEmployeeVM Login(LoginVM loginVM);

}

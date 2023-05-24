using WebAPI.Model;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;

namespace WebAPI.Contracts;

public interface IAccountRepository : IGenericRepository<Account>
{
    LoginVM Login(LoginVM loginVM);

}

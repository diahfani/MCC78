using WebAPI.Model;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;

namespace WebAPI.Contracts;

public interface IAccountRepository : IGenericRepository<Account>
{
    LoginVM Login(LoginVM loginVM);
    int Register(RegisterVM registerVM);
    int UpdateOTP(Guid? employeeId);
    int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);

}

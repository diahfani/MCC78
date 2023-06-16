using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface;

public interface IAccountRepository : IRepository<Account, Guid>
{
    public Task<ResponseVM<string>> Login(LoginVM loginVM);
    public Task<ResponseMessageVM> Register(RegisterVM registerVM);
}

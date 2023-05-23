using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories;

public class AccountRoleRepository : GenericRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(BookingRoomsDBContext context) : base(context)
    {
    }

}

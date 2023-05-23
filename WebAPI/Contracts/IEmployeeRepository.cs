using WebAPI.Model;

namespace WebAPI.Contracts;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    /*IEnumerable<Employee> GetByGuidAcc(Guid acId);*/
    IEnumerable<Employee> GetByEmail(string email);
}

using WebAPI.Model;
using WebAPI.ViewModels.Employees;

namespace WebAPI.Contracts;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    /*IEnumerable<Employee> GetByGuidAcc(Guid acId);*/
    EmployeeVM GetByEmail(string email);
    IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
    MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);
    int CreateWithValidate(Employee employee);
    bool CheckEmailAndPhoneAndNIK(string value);
    Guid? FindGuidByEmail(string email);
}

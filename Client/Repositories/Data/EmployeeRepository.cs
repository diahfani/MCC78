using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Repositories.Data;

public class EmployeeRepository : GeneralRepository<Employee, Guid>, IEmployeeRepository
{
    private readonly string request;
    private readonly HttpClient httpClient;
    public EmployeeRepository(string request = "Employee/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7159/api/")
        };
        this.request = request;
    }

     public async Task<ResponseListVM<MasterEmployeeVM>> GetAllMasterEmployee()
    {
        ResponseListVM<MasterEmployeeVM> responseListVM = null;
        using (var response = await httpClient.GetAsync(request + "GetAllMasterEmployee"))
        {
            string apiResponse =  await response.Content.ReadAsStringAsync();
            responseListVM = JsonConvert.DeserializeObject<ResponseListVM<MasterEmployeeVM>>(apiResponse);
        }
        return responseListVM;
    }
}

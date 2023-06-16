using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http;
using System.Text;

namespace Client.Repositories.Data;

public class AccountRepository : GeneralRepository<Account, Guid>, IAccountRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;
    public AccountRepository(string request = "Account/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7159/api/")
        };
        this.request = request;
    }

    public async Task<ResponseVM<string>> Login(LoginVM loginVM)
    {
        ResponseVM<string> responseVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "Login", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseVM = JsonConvert.DeserializeObject<ResponseVM<string>>(apiResponse);
        }
        return responseVM;
    }

    public async Task<ResponseMessageVM> Register(RegisterVM registerVM)
    {
        ResponseMessageVM responseVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "Register", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
        }
        return responseVM;
    }
}

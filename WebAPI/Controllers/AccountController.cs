using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Account, AccountVM> _mapper;
    public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper, IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var account = _accountRepository.GetAll();
        if (!account.Any())
        {
            return NotFound();
        }
        var resultConverted = account.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginVM loginVM)
    {
        var query = _accountRepository.Login(loginVM);

        if (query == null)
        {
            return BadRequest("Account not found");
        }

        if (query.Password != loginVM.Password)
        {
            return BadRequest("Password not match");
        }
        return Ok();
       
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(account);

        return Ok(data);
        
    }

    [HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var result = _accountRepository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConvert = _mapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(accountConvert);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}

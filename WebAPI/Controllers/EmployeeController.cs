using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Booking;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper<Employee, EmployeeVM> _mapper;
    private readonly IMapper<Account, AccountVM> _accountMapper;
    public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper,
        IAccountRepository accountRepository, IMapper<Account, AccountVM> accountMapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _accountMapper = accountMapper;
        _accountRepository = accountRepository;
    }

    [HttpGet("byEmail/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var emailEmployee = _employeeRepository.GetByEmail(email);
        if (emailEmployee is null)
        {
            return NotFound();
        }
        return Ok(emailEmployee);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var employee = _employeeRepository.GetAll();
        if (!employee.Any())
        {
            return NotFound();
        }
        var resultConverted = employee.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(employee);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var result = _employeeRepository.Create(employeeConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var isUpdated = _employeeRepository.Update(employeeConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }


}

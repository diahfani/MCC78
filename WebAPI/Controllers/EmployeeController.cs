using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Booking;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Others;
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
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        return Ok(new ResponseVM<IEnumerable<Employee>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = emailEmployee,
        });
    }

    [HttpGet]
    public IActionResult GetEmployee()
    {
        var employee = _employeeRepository.GetAll();
        if (!employee.Any())
        {
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = employee.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<EmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = resultConverted,
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound(new ResponseVM<EmployeeVM> 
            { 
                Code = StatusCodes.Status404NotFound, 
                Status = HttpStatusCode.NotFound.ToString(), 
                Message = "Data not found", 
                Data = null 
            });
        }
        var data = _mapper.Map(employee);

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = data,
        });
    }

    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAll()
    {
        var masterEmployees = _employeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound(new ResponseVM<EmployeeVM> 
            { 
                Code = StatusCodes.Status404NotFound, 
                Status = HttpStatusCode.NotFound.ToString(), 
                Message = "Data not found", 
                Data = null 
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = masterEmployees,
        });
    }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid(Guid guid)
    {
        var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
        if (masterEmployees is null)
        {
            return NotFound(new ResponseVM<MasterEmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<MasterEmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = masterEmployees,
        });
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var result = _employeeRepository.Create(employeeConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var isUpdated = _employeeRepository.Update(employeeConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }
    

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success",
            Data = null,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }
    

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }


}

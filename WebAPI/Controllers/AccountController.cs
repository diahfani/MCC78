using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Others;
using WebAPI.Repositories;
using WebAPI.Utility;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Login;
using WebAPI.ViewModels.Universities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : GenericController<Account, AccountVM>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    /*private readonly IMapper<Account, AccountVM> _mapper;*/
    public AccountController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper,
        IEmailService emailService, ITokenService tokenService) : base(accountRepository, mapper)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _emailService = emailService;
        _tokenService = tokenService;
        /*_mapper = mapper;*/
    }

    /*[HttpGet]
    public IActionResult GetAll()
    {
        var account = _repository.GetAll();
        if (!account.Any())
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = account.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<AccountVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully obtained!",
            Data = resultConverted,
        });
    }
*/
    [HttpPost("login")]
    public IActionResult Login(LoginVM loginVM)
    {
        var employees = _employeeRepository.GetByEmail(loginVM.Email);
        if (employees == null)
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not found"
            });
        }

        var employee = new EmployeeVM
        {
            Nik = employees.Nik,
            Email = employees.Email,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            BirthDate = employees.BirthDate,
            Gender = employees.Gender.ToString(),
            HiringDate = employees.HiringDate,
            PhoneNumber = employees.PhoneNumber,
        };

        var query = _accountRepository.Login(loginVM);
        if (query == null)
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Email not found",
                Data = null
            });
        }
        var validatePassword = Hashing.ValidatePassword(loginVM.Password, query.Password);
        if (validatePassword is false)
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password didn't match",
                Data = null
            });
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, employee.Nik),
            new(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
            new(ClaimTypes.Email, employee.Email)
        };

        var roles = _accountRepository.GetRoles(employee.Guid);

        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = _tokenService.GenerateToken(claims);

        return Ok(new ResponseVM<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success login!",
            Data = token
        });

    }

    [HttpPost("Register")]
    public IActionResult Register(RegisterVM registerVM)
    {

        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Registration failed",
                    Data = null
                }); ;
            case 1:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email already exist",
                    Data = null
                });
            case 2:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Phone number already exist",
                    Data = null
                });
            case 3:
                return Ok(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success register!",
                    Data = null
                });
        }

        return BadRequest(new ResponseVM<RegisterVM>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Failed",
            Data = null
        });

    }

    [HttpPost("ForgotPassword/{email}")]
    public IActionResult UpdateResetPass(String email)
    {

        var getGuid = _employeeRepository.FindGuidByEmail(email);
        if (getGuid == null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found",
                Data = null
            });
        }

        var isUpdated = _accountRepository.UpdateOTP(getGuid);

        switch (isUpdated)
        {
            case 0:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed",
                    Data = null
                });
            default:
                /*var hasil = new AccountResetPasswordVM
                {
                    Email = email,
                    OTP = isUpdated
                };*/

                _emailService.SetEmail(email)
                    .SetSubject("Forgot Password")
                    .SetHtmlMessage($"Your OTP is {isUpdated}")
                    .SendEmailAsync();

                return Ok(new ResponseVM<AccountResetPasswordVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "OTP successfully has been sent to email",
                });

        }


    }

    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
    {
        // Cek apakah email dan OTP valid
        var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
        var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
        switch (changePass)
        {
            case 0:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed",
                    Data = null
                });
            case 1:
                return Ok(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Password has been changed successfully",
                    Data = null,
                });
            case 2:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Invalid OTP",
                    Data = null
                });
            case 3:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP has been used",
                    Data = null
                });
            case 4:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP failed",
                    Data = null
                });
            case 5:
                return BadRequest(new ResponseVM<AccountVM> 
                { 
                    Code = StatusCodes.Status400BadRequest, 
                    Status = HttpStatusCode.BadRequest.ToString(), 
                    Message = "Password didn't match", 
                    Data = null 
                });
            default:
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed",
                    Data = null
                });
        }



    }

    [HttpGet("token")]
    public IActionResult GetByToken(string token)
    {
        var data = _tokenService.ExtractClaimsFromJwt(token);
        if (data == null)
        {
            return NotFound(new ResponseVM<ClaimVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        return Ok(new ResponseVM<ClaimVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Success",
            Data = data
        });
    }
    /*[HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var account = _repository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var data = _mapper.Map(account);

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully obtained!",
            Data = data,
        });

    }
*/
    /*[HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var result = _repository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success create!",
            Data = null,
        });
    }
*/
    /*[HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConvert = _mapper.Map(accountVM);
        var isUpdated = _repository.Update(accountConvert);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<List<AccountVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully updated!",
            Data = null,
        });
    }
*/
    /*[HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _repository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<List<AccountVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully deleted!",
            Data = null,
        });
    }*/

}

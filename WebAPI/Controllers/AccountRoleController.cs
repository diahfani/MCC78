using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Others;
using WebAPI.ViewModels.AccountRoles;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : GenericController<AccountRole, AccountRoleVM>
{
    /*private readonly IAccountRoleRepository _accountRoleRepository;*/
    /*private readonly IMapper<AccountRole, AccountRoleVM> _mapper;*/
    public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper) : base(accountRoleRepository, mapper)
    {
        /*_accountRoleRepository = accountRoleRepository;*/
        /*_mapper = mapper;*/
    }

    /*[HttpGet]
    public IActionResult GetAll()
    {
        var accountRole = _accountRoleRepository.GetAll();
        if (!accountRole.Any())
        {
            return NotFound(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = accountRole.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<AccountRoleVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully obtained!",
            Data = resultConverted,
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null)
        {
            return NotFound(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var data = _mapper.Map(accountRole);

        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully obtained!",
            Data = data,
        });
    }

    [HttpPost]
    public IActionResult Create(AccountRoleVM accountRoleVM)
    {
        var accountRoleConverted = _mapper.Map(accountRoleVM);
        var result = _accountRoleRepository.Create(accountRoleConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(AccountRoleVM accountRoleVM)
    {
        var accountRoleConverted = _mapper.Map(accountRoleVM);
        var isUpdated = _accountRoleRepository.Update(accountRoleConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<AccountRoleVM>
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
        var isDeleted = _accountRoleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }*/

}

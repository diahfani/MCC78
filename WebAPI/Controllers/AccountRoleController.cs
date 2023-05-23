using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.AccountRoles;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : ControllerBase
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
    public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountRole = _accountRoleRepository.GetAll();
        if (!accountRole.Any())
        {
            return NotFound();
        }
        var resultConverted = accountRole.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(accountRole);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(AccountRoleVM accountRoleVM)
    {
        var accountRoleConverted = _mapper.Map(accountRoleVM);
        var result = _accountRoleRepository.Create(accountRoleConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountRoleVM accountRoleVM)
    {
        var accountRoleConverted = _mapper.Map(accountRoleVM);
        var isUpdated = _accountRoleRepository.Update(accountRoleConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRoleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}

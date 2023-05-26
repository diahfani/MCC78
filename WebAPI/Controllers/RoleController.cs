using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Others;
using WebAPI.ViewModels.Roles;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RoleVM> _mapper;
    public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var role = _roleRepository.GetAll();
        if (!role.Any())
        {
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = role.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<RoleVM>>
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
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var data = _mapper.Map(role);

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = data,
        });
    }

    [HttpPost]
    public IActionResult Create(RoleVM roleVM)
    {
        var roleConverted = _mapper.Map(roleVM);
        var result = _roleRepository.Create(roleConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(RoleVM roleVM)
    {
        var roleConverted = _mapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(roleConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<RoleVM>
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
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<RoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }
}

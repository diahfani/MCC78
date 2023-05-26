using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Utility;
using WebAPI.ViewModels.Bookings;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Others;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationVM> _educationMapper;
    public EducationController(IEducationRepository educationRepository, IMapper<Education, EducationVM> educationMapper)
    {
        _educationRepository = educationRepository;
        _educationMapper = educationMapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var education = _educationRepository.GetAll();
        if (!education.Any())
        {
            return NotFound(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var data = education.Select(_educationMapper.Map).ToList();
        return Ok(new ResponseVM<List<EducationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success get data",
            Data = data,
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var education = _educationRepository.GetByGuid(guid);
        if (education is null)
        {
            return NotFound(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var data = _educationMapper.Map(education);

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success get data",
            Data = data,
        });

    }

    [HttpPost]
    public IActionResult Create(EducationVM educationVM)
    {
        var educationConvert = _educationMapper.Map(educationVM);
        var result = _educationRepository.Create(educationConvert);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(EducationVM educationVM)
    {
        var educationConvert = _educationMapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationConvert);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<EducationVM>
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
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EducationVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<EducationVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }

}

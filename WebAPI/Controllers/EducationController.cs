using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Utility;
using WebAPI.ViewModels.Educations;
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
            return NotFound();
        }
        var data = education.Select(_educationMapper.Map).ToList();
        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var education = _educationRepository.GetByGuid(guid);
        if (education is null)
        {
            return NotFound();
        }
        var data = _educationMapper.Map(education);

        return Ok(data);

    }

    [HttpPost]
    public IActionResult Create(EducationVM educationVM)
    {
        var educationConvert = _educationMapper.Map(educationVM);
        var result = _educationRepository.Create(educationConvert);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(EducationVM educationVM)
    {
        var educationConvert = _educationMapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationConvert);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}

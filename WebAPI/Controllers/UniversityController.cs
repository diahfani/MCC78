using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<University, UniversityVM> _mapper;
    private readonly IMapper<Education, EducationVM> _educationMapper;
    public UniversityController(IUniversityRepository universityRepository, IEducationRepository educationRepository, IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationMapper = educationMapper;
    }

    [HttpGet("WithEducation")]

    public IActionResult GetAllWithEducation()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound();
        }

        var resultMapped = new List<UniversityEducationVM>();
        foreach (var university in universities)
        {
            var education = _educationRepository.GetByUniversityGuid(university.Guid);
            var educationMapped = education.Select(_educationMapper.Map);

            var result = new UniversityEducationVM
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name,
                Educations = educationMapped,
            };

            resultMapped.Add(result);
            
        }
        return Ok(resultMapped);
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any()) {
            return NotFound();
        }

        // cara manual
        /*var universitiesToVM = new List<UniversityVM>();
        foreach(var university in universities)
        {
            var result = UniversityVM.ToVM(university);
            universitiesToVM.Add(result);
        }*/

        //cara cepet pake LINQ
        var resultConverted = universities.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid:guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return NotFound();
        }

        var data = _mapper.Map(university);

        return Ok(data);
    }

    [HttpGet("byName/{name}")]
    public IActionResult GetByName(string name)
    {
        var nameUniversity = _universityRepository.GetByName(name);
        if (nameUniversity is null)
        {
            return NotFound();
        }
        return Ok(nameUniversity);
    }

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        /*var universityConverted = UniversityVM.ToModel(universityVM);*/
        var universityConverted = _mapper.Map(universityVM);
        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(UniversityVM universityVM)
    {
        var universityConvert = _mapper.Map(universityVM);
        var isUpdated = _universityRepository.Update(universityConvert);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

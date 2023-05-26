using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Educations;
using WebAPI.ViewModels.Others;
using WebAPI.ViewModels.Roles;
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
            return NotFound(new ResponseVM<UniversityEducationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
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
        return Ok(new ResponseVM<IEnumerable<UniversityEducationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = resultMapped,
        });
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any()) {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
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

        return Ok(new ResponseVM<List<UniversityVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success get data",
            Data = resultConverted,
        });
    }

    [HttpGet("{guid:guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        var data = _mapper.Map(university);

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = data,
        });
    }

    /*[HttpGet("byName/{name}")]
    public IActionResult GetByName(string name)
    {
        var nameUniversity = _universityRepository.GetByName(name);
        if (nameUniversity is null)
        {
            return NotFound(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        return Ok(new ResponseVM<IEnumerable<UniversityVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = nameUniversity,
        });
    }*/

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        /*var universityConverted = UniversityVM.ToModel(universityVM);*/
        var universityConverted = _mapper.Map(universityVM);
        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(UniversityVM universityVM)
    {
        var universityConvert = _mapper.Map(universityVM);
        var isUpdated = _universityRepository.Update(universityConvert);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<UniversityVM>
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
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<UniversityVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }
}

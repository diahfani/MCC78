using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Repositories;
using WebAPI.Utility;
using WebAPI.ViewModels.Accounts;
using WebAPI.ViewModels.Others;

namespace WebAPI.Controllers;

public abstract class GenericController<TEntity, TViewModel> : ControllerBase
    where TEntity : class
    where TViewModel : class
    
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper<TEntity, TViewModel> _mapper;
    public GenericController(IGenericRepository<TEntity> repository, IMapper<TEntity, TViewModel> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        var item = _repository.GetAll();
        if (!item.Any())
        {
            return NotFound(new ResponseVM<TViewModel>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = item.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<TViewModel>>
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
        var item = _repository.GetByGuid(guid);
        if (item is null)
        {
            return NotFound(new ResponseVM<TViewModel>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var data = _mapper.Map(item);

        return Ok(new ResponseVM<TViewModel>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully obtained!",
            Data = data,
        });

    }

    [HttpPost]
    public IActionResult Create(TViewModel viewModel)
    {
        var itemConverted = _mapper.Map(viewModel);
        var result = _repository.Create(itemConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<TViewModel>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<TViewModel>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success create!",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(TViewModel viewModel)
    {
        var itemConverted = _mapper.Map(viewModel);
        var isUpdated = _repository.Update(itemConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<TViewModel>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<List<TViewModel>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully updated!",
            Data = null,
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _repository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<TViewModel>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<List<TViewModel>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data successfully deleted!",
            Data = null,
        });
    }

    /*    private readonly GenericRepository<TEntity> _repository;*/


    /*    public GenericController(GenericRepository<TEntity> repository)
        {
            _repository = repository;
        }*/
    /*
        [HttpGet]
        public IActionResult GetAll()
        {
            var account = _repository.GetAll();
            if (!account.Any())
            {
                return NotFound();
            }

            return Ok(account);
        }*/


}

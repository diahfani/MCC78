using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Contracts;
using WebAPI.Repositories;
using WebAPI.Utility;

namespace WebAPI.Controllers;

public abstract class GenericController<TEntity, TRepo, TViewModel> : ControllerBase
    where TEntity : class
    where TRepo : IGenericRepository<TEntity>
    where TViewModel : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper<TEntity, TViewModel> _mapper;

    public GenericController(IGenericRepository<TEntity> repo, IMapper<TEntity, TViewModel> mapper)
    {
        _repository = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entity = _repository.GetAll();
        if (!entity.Any())
        {
            return NotFound();
        }
        return Ok(entity);
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

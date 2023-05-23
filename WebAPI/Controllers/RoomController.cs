using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper<Room, RoomVM> _mapper;
    public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var room = _roomRepository.GetAll();
        if (!room.Any())
        {
            return NotFound();
        }
        var resultConverted = room.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }

        var data = _mapper.Map(room);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(RoomVM roomVM)
    {
        var roomConvert = _mapper.Map(roomVM);
        var result = _roomRepository.Create(roomConvert);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(RoomVM roomVM)
    {
        var roomConvert = _mapper.Map(roomVM);
        var isUpdated = _roomRepository.Update(roomConvert);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roomRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }



}

using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Others;
using WebAPI.ViewModels.Roles;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RoomController : GenericController<Room, RoomVM>
{
    private readonly IRoomRepository _roomRepository;
    public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper) : base(roomRepository, mapper)
    {
        _roomRepository = roomRepository;
    }

/*    [HttpGet]
    public IActionResult GetAll()
    {
        var room = _roomRepository.GetAll();
        if (!room.Any())
        {
            return NotFound(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = room.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<RoomVM>>
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
        var room = _roomRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        var data = _mapper.Map(room);
        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = data,
        });
    }*/

    [HttpGet("CurrentlyUsedRooms")]
    public IActionResult GetCurrentlyUsedRooms()
    {
        var room = _roomRepository.GetCurrentlyUsedRooms();
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomUsedVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = room,
        });
    }

    [HttpGet("CurrentlyUsedRoomsByDate")]
    public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
    {
        var room = _roomRepository.GetByDate(dateTime);
        if (room is null)
        {
            return NotFound(new ResponseVM<RoomUsedVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "success get data",
            Data = room,
        });
    }

    [HttpGet("RoomAvailable")]
    public IActionResult GetRoomByDate()
    {
        try
        {
            var room = _roomRepository.GetRoomByDate();
            if (room is null)
            {
                return NotFound(new ResponseVM<RoomBookedTodayVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<RoomBookedTodayVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "success get data",
                Data = room,
            });
        }
        catch
        {
            return BadRequest(new ResponseVM<RoomBookedTodayVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Get data failed",
                Data = null
            });
        }
    }

/*    [HttpPost]
    public IActionResult Create(RoomVM roomVM)
    {
        var roomConvert = _mapper.Map(roomVM);
        var result = _roomRepository.Create(roomConvert);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(RoomVM roomVM)
    {
        var roomConvert = _mapper.Map(roomVM);
        var isUpdated = _roomRepository.Update(roomConvert);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<RoomVM>
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
        var isDeleted = _roomRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RoomVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<RoomVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }
*/


}

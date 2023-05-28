using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.AccountRoles;
using WebAPI.ViewModels.Booking;
using WebAPI.ViewModels.Bookings;
using WebAPI.ViewModels.Others;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : GenericController<Booking, BookingVM> 
{
    private readonly IBookingRepository _bookingRepository;
    /*private readonly IMapper<Booking, BookingVM> _mapper;*/
    public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> mapper) : base(bookingRepository, mapper)
    {
        _bookingRepository = bookingRepository;
        /*_mapper = mapper;*/
    }

    /*[HttpGet]
    public IActionResult GetAll()
    {
        var booking = _bookingRepository.GetAll();
        if (!booking.Any())
        {
            return NotFound(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }
        var resultConverted = booking.Select(_mapper.Map).ToList();

        return Ok(new ResponseVM<List<BookingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success get data",
            Data = resultConverted,
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return NotFound(new ResponseVM<BookingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        var data = _mapper.Map(booking);
        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success get data",
            Data = data,
        });
    }*/

    [HttpGet("bookingduration")]
    public IActionResult GetDuration()
    {
        var bookingLengths = _bookingRepository.GetBookingDuration();
        if (!bookingLengths.Any())
        {
            return NotFound(new ResponseVM<BookingDurationVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found",
                Data = null
            });
        }

        return Ok(new ResponseVM<IEnumerable<BookingDurationVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success get data",
            Data = bookingLengths,
        });
    }

    [HttpGet("BookingDetail")]
    public IActionResult GetAllBookingDetail()
    {
        try
        {

            var results = _bookingRepository.GetAllBookingDetail();

            return Ok(new ResponseVM<IEnumerable<BookingDetailVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success get data",
                Data = results,
            });
        }
        catch
        {
            return BadRequest();
        }

    }

    [HttpGet("BookingDetail/{guid}")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        try
        {
            var bookingDetailVM = _bookingRepository.GetBookingDetailByGuid(guid);

            if (bookingDetailVM is null)
            {
                return NotFound(new ResponseVM<BookingDetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found",
                    Data = null
                });
            }


            return Ok(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success get data",
                Data = bookingDetailVM,
            });
        }
        catch
        {
            return BadRequest(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed get data",
                Data = null
            });
        }
    }



    /*[HttpPost]
    public IActionResult Create(BookingVM bookingVM)
    {
        var bookingConverted = _mapper.Map(bookingVM);
        var result = _bookingRepository.Create(bookingConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = null,
        });
    }

    [HttpPut]
    public IActionResult Update(BookingVM bookingVM)
    {
        var bookingConverted = _mapper.Map(bookingVM);
        var isUpdated = _bookingRepository.Update(bookingConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<BookingVM>
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
        var isDeleted = _bookingRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<BookingDetailVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Delete failed",
                Data = null
            });
        }

        return Ok(new ResponseVM<BookingVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = null,
        });
    }
*/
}

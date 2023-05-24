﻿using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Booking;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase 
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper<Booking, BookingVM> _mapper;
    public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var booking = _bookingRepository.GetAll();
        if (!booking.Any())
        {
            return NotFound();
        }
        var resultConverted = booking.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking is null)
        {
            return NotFound();
        }

        var data = _mapper.Map(booking);
        return Ok(data);
    }

    [HttpGet("bookinglength")]
    public IActionResult GetBookingLength()
    {
        var bookingLengths = _bookingRepository.GetBookingLength();
        if (!bookingLengths.Any())
        {
            return NotFound();
        }

        return Ok(bookingLengths);
    }

    [HttpPost]
    public IActionResult Create(BookingVM bookingVM)
    {
        var bookingConverted = _mapper.Map(bookingVM);
        var result = _bookingRepository.Create(bookingConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(BookingVM bookingVM)
    {
        var bookingConverted = _mapper.Map(bookingVM);
        var isUpdated = _bookingRepository.Update(bookingConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _bookingRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}

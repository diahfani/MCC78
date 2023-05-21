﻿using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IGenericRepository<Role> _roleRepository;
    public RoleController(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var role = _roleRepository.GetAll();
        if (!role.Any())
        {
            return NotFound();
        }
        return Ok(role);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public IActionResult Create(Role role)
    {
        var result = _roleRepository.Create(role);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Role role)
    {
        var isUpdated = _roleRepository.Update(role);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

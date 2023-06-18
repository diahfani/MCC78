using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var employee = new List<Employee>();

        if (result.Data != null) 
        {
            employee = result.Data?.Select(e => new Employee
            {
                Guid = e.Guid,
                Nik = e.Nik,
                FirstName = e.FirstName,
                LastName = e.LastName,
                BirthDate = e.BirthDate,
                Gender = e.Gender,
                HiringDate = e.HiringDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
            }).ToList();
        } 
        
        return View(employee);
    }

    public async Task<IActionResult> GetAllMasterEmployee()
    {
        var result = await repository.GetAllMasterEmployee();
        var employee = new List<MasterEmployeeVM>();

        if (result.Data != null)
        {
            employee = result.Data?.Select(e => new MasterEmployeeVM
            {
                Guid = e.Guid,
                NIK = e.NIK,
                FullName = e.FullName,
                BirthDate = e.BirthDate,
                Gender = e.Gender.ToString(),
                HiringDate = e.HiringDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Major = e.Major,
                Degree = e.Degree,
                GPA = e.GPA,
                UniversityName = e.UniversityName,
            }).ToList();
        }

        return View(employee);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }


    [HttpPost]
    /*[ValidateAntiForgeryToken]*/
    public async Task<IActionResult> Create(Employee employee)
    {
        /*if (ModelState.IsValid)
        {*/
        var result = await repository.Post(employee);
        if (result.StatusCode == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        /*}*/
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    /*[ValidateAntiForgeryToken]*/
    public async Task<IActionResult> Edit(Guid guid)
    {

        var result = await repository.Get(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.Nik = result.Data.Nik;
            employee.FirstName = result.Data.FirstName;
            employee.LastName = result.Data.LastName;
            employee.BirthDate = result.Data.BirthDate;
            employee.Gender = result.Data.Gender;   
            employee.HiringDate = result.Data.HiringDate;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;        }

        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        var result = await repository.Put(employee);
        if (result.StatusCode == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        /* }*/
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await repository.Get(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.Nik = result.Data.Nik;
            employee.FirstName = result.Data.FirstName; 
            employee.LastName = result.Data.LastName;
            employee.BirthDate = result.Data.BirthDate;
            employee.Gender = result.Data.Gender;
            employee.HiringDate = result.Data.HiringDate;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;
        }
        return View(employee);
    }

    [HttpPost]
    /*[ValidateAntiForgeryToken]*/
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await repository.Delete(guid);
        if (result.StatusCode == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }


}

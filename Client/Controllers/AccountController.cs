﻿using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository repository;

    public AccountController(IAccountRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        var result = await repository.Login(loginVM);
/*        HttpContext.Session.SetString("Email", loginVM.Email);
*/        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        } else if (result.Code == 409) {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        } else if (result.Code == 200)
        {
            HttpContext.Session.SetString("JWToken", result.Data);
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Register() {
        return View();
    
    }

    [HttpGet("/Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("/Account/Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM reg)
    {
        var result = await repository.Register(reg);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.StatusCode == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
            return View();
        }
        else if (result.StatusCode == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction("GetAllMasterEmployee", "Employee");
        }
        return RedirectToAction("GetAllMasterEmployee", "Employee");
        /*return View();*/

    }
}

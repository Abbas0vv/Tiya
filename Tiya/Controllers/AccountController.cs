using Microsoft.AspNetCore.Mvc;
using Tiya.Database.DomainModels.Account;
using Tiya.Database.Interfaces;
using Tiya.Database.ViewModels;

namespace Tiya.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _userRepository.Register(model);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _userRepository.Login(model);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _userRepository.LogOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> CreateRole()
    {
        await _userRepository.CreateRole();
        return RedirectToAction("Index", "Home");
    }
}

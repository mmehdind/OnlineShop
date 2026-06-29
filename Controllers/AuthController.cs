using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Auth;
using OnlineShop.Services.Interfaces;
using AutoMapper;
using OnlineShop.ViewModels.Auth;

namespace OnlineShop.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm registerVm)
    {

        if (!ModelState.IsValid)
            return View(registerVm);

        var dto = _mapper.Map<RegisterDto>(registerVm);

        var result = await _authService.RegisterAsync(dto);

        if (!result.Succeeded) 
        { 
            foreach (var error in result.Errors) 
            { 
                ModelState.AddModelError("", error.Description); 
            } 
            
            return View(registerVm); 
        } 

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm loginVm)
    {

        if (!ModelState.IsValid)
            return View(loginVm);

        

        var dto = _mapper.Map<LoginDto>(loginVm);

        var satsus = await _authService.LoginAsync(dto);

        if (satsus) return RedirectToAction("Index", "Home");

        TempData["Error"] = "نام کاربری یا رمز عبور اشتباه است.";
        return RedirectToAction(nameof(Login));
        
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }
}
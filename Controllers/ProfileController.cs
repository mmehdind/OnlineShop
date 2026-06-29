using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Address;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IAddressService _addressService;

    public ProfileController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    // ================= PROFILE HOME =================

    public IActionResult Index()
    {
        return View();
    }

    // ================= ADDRESSES =================

    [HttpGet]
    public async Task<IActionResult> Addresses()
    {
        var addresses = await _addressService.GetUserAddressesAsync();
        return View(addresses);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDto dto)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Addresses));

        await _addressService.CreateAsync(dto);

        return RedirectToAction(nameof(Addresses));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddress(UpdateAddressDto dto)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Addresses));

        await _addressService.UpdateAsync(dto);

        return RedirectToAction(nameof(Addresses));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        await _addressService.DeleteAsync(id);
        return RedirectToAction(nameof(Addresses));
    }

    [HttpPost]
    public async Task<IActionResult> SetDefault(int id)
    {
        await _addressService.SetDefaultAsync(id);
        return RedirectToAction(nameof(Addresses));
    }
}
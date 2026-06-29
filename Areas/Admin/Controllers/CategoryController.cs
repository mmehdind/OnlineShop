using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Admin.ViewModels.Category;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly IAdminCategoryService _adminCategoryService;

    public CategoryController(IAdminCategoryService admin)
    {
        _adminCategoryService = admin;
    }

    // LIST
    public async Task<IActionResult> Index()
        => View(await _adminCategoryService.GetListAsync());

    // CREATE
    public async Task<IActionResult> Create()
        => View(await _adminCategoryService.GetCreateVmAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryAdminVm vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        await _adminCategoryService.CreateAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    // EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var vm = await _adminCategoryService.GetEditVmAsync(id);
        return vm == null ? NotFound() : View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateCategoryAdminVm vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        await _adminCategoryService.UpdateAsync(vm);
        return RedirectToAction(nameof(Index));
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _adminCategoryService.SoftDeleteAsync(id);
        return Ok();
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Admin.ViewModels.Product;
using OnlineShop.Common.Queries;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IAdminProductService _adminProductService;

    public ProductController(IAdminProductService admin)
    {
        _adminProductService = admin;
    }

    // LIST
    public async Task<IActionResult> Index(ProductQuery query)
    {
        var vm = await _adminProductService.GetListAsync(query);

        return View(vm);
    }

    // CREATE - GET
    public async Task<IActionResult> Create()
    {
        var vm = await _adminProductService.GetCreateVmAsync();

        return View(vm);
    }

    // CREATE - POST
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVm vm)
    {
        if (!ModelState.IsValid)
            return View(await _adminProductService.GetCreateVmAsync());

        await _adminProductService.CreateAsync(vm);

        return RedirectToAction(nameof(Index), new { area = "Admin" });
    }

    // DETAILS
    public async Task<IActionResult> Details(int id)
    {
        var product = await _adminProductService.GetDetailsAsync(id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    // EDIT - GET
    public async Task<IActionResult> Edit(int id)
    {
        var vm = await _adminProductService.GetEditVmAsync(id);

        if (vm == null)
            return NotFound();

        return View(vm);
    }

    // EDIT - POST
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateProductVm vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        await _adminProductService.UpdateAsync(vm);

        return Ok();
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _adminProductService.DeleteAsync(id);

        return Ok();
    }

    // IMAGE : UPLOAD
    [HttpPost]
    public async Task<IActionResult> UploadImage(int productId, IFormFile file)
    {
        await _adminProductService.AddImageAsync(productId, file);

        return RedirectToAction(nameof(Edit), new { id = productId });
    }

    // IMAGE : DELETE
    [HttpPost]
    public async Task<IActionResult> DeleteImage(int imageId, int productId)
    {
        await _adminProductService.DeleteImageAsync(imageId);

        return RedirectToAction(nameof(Edit), new { id = productId });
    }

    // IMAGE : SET MAIN
    [HttpPost]
    public async Task<IActionResult> SetMainImage(int imageId, int productId)
    {
        await _adminProductService.SetMainImageAsync(imageId);

        return Ok();
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Common.Queries;
using OnlineShop.Admin.DTOs.Order;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly IAdminOrderService _adminOrderService;

    public OrderController(IAdminOrderService adminOrderService)
    {
        _adminOrderService = adminOrderService;
    }

    // LIST + FILTER
    public async Task<IActionResult> Index(OrderQuery query)
    {
        var vm = await _adminOrderService.GetListAsync(query);
        return View(vm);
    }

    // DETAILS
    public async Task<IActionResult> Details(int id)
    {
        var order = await _adminOrderService.GetDetailsAsync(id);

        if (order == null)
            return NotFound();

        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeOrderStatusDto dto)
    {
        await _adminOrderService.ChangeStatusAsync(dto);

        return Ok();
    }
}
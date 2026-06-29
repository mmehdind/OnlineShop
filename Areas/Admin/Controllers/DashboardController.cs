using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Admin.DTOs.Dashboard;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IAdminDashboardService _dashboardService;

    public DashboardController(IAdminDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    // GET: /Admin/Dashboard
    public async Task<IActionResult> Index(ChartPeriod? period)
    {
        var selectedPeriod = period ?? ChartPeriod.Daily;

        var data = await _dashboardService.GetDashboardAsync(selectedPeriod);

        return View(data);
    }

    // API endpoint for chart update (AJAX)
    [HttpGet]
    public async Task<IActionResult> SalesChart(ChartPeriod period)
    {
        var data = await _dashboardService.GetDashboardAsync(period);

        return Json(data.SalesChart);
    }
}
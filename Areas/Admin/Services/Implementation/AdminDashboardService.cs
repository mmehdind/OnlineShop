using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Admin.DTOs.Dashboard;
using OnlineShop.Admin.ViewModels.Dashboard;
using OnlineShop.Data.Context;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AdminDashboardService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DashboardVm> GetDashboardAsync(ChartPeriod period)
    {
        var now = DateTime.UtcNow;

        var stats = await GetStatsAsync(now);
        var recentOrders = await GetRecentOrdersAsync();
        var lowStock = await GetLowStockAsync();
        var chart = await GetSalesChartAsync(period, now);

        var dto = new DashboardDto
        {
            Stats = stats,
            RecentOrders = recentOrders,
            LowStockProducts = lowStock,
            SalesChart = chart
        };

        return _mapper.Map<DashboardVm>(dto);
    }

        private async Task<DashboardStatsDto> GetStatsAsync(DateTime now)
    {
        var today = now.Date;
        var month = new DateTime(now.Year, now.Month, 1);

        return new DashboardStatsDto
        {
            TotalProducts = await _context.Products.CountAsync(),
            TotalCategories = await _context.Categories.CountAsync(),
            TotalUsers = await _context.Users.CountAsync(),

            TodayOrders = await _context.Orders
                .CountAsync(x => x.CreatedAt >= today),

            PendingOrders = await _context.Orders
                .CountAsync(x => x.Status == OrderStatus.Pending),

            ProcessingOrders = await _context.Orders
                .CountAsync(x => x.Status == OrderStatus.Processing),

            TotalRevenue = await _context.Orders
                .Where(x => x.Status == OrderStatus.Paid)
                .SumAsync(x => (long?)x.TotalPrice) ?? 0,

            MonthlyRevenue = await _context.Orders
                .Where(x => x.Status == OrderStatus.Paid && x.CreatedAt >= month)
                .SumAsync(x => (long?)x.TotalPrice) ?? 0
        };
    }

        private async Task<List<RecentOrderDto>> GetRecentOrdersAsync()
    {
        return await _context.Orders
            .Include(x => x.AppUser)
            .OrderByDescending(x => x.CreatedAt)
            .Take(10)
            .Select(x => new RecentOrderDto
            {
                Id = x.Id,
                UserName = x.AppUser.UserName ?? "Unknown",
                TotalPrice = x.TotalPrice,
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

        private async Task<List<LowStockProductDto>> GetLowStockAsync()
    {
        return await _context.Products
            .Where(x => x.Stock <= 5)
            .OrderBy(x => x.Stock)
            .Select(x => new LowStockProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Stock = x.Stock
            })
            .ToListAsync();
    }

        private async Task<SalesChartDto> GetSalesChartAsync(
        ChartPeriod period,
        DateTime now)
    {
        DateTime from;

        if (period == ChartPeriod.Daily)
            from = now.AddDays(-7);

        else if (period == ChartPeriod.Weekly)
            from = now.AddDays(-30);

        else
            from = now.AddMonths(-6);

        var orders = await _context.Orders
            .Where(x => x.Status == OrderStatus.Paid && x.CreatedAt >= from)
            .ToListAsync();

        var grouped = period switch
        {
            ChartPeriod.Daily => orders
                .GroupBy(x => x.CreatedAt.ToString("yyyy-MM-dd")),

            ChartPeriod.Weekly => orders
                .GroupBy(x =>
                {
                    var cal = System.Globalization.CultureInfo.InvariantCulture.Calendar;
                    var week = cal.GetWeekOfYear(
                        x.CreatedAt,
                        System.Globalization.CalendarWeekRule.FirstDay,
                        DayOfWeek.Saturday);

                    return $"{x.CreatedAt.Year}-W{week}";
                }),

            ChartPeriod.Monthly => orders
                .GroupBy(x => x.CreatedAt.ToString("yyyy-MM")),

            _ => orders.GroupBy(x => x.CreatedAt.ToString("yyyy-MM-dd"))
        };

        return new SalesChartDto
        {
            Period = period,
            Items = grouped.Select(g => new SalesChartItemDto
            {
                Label = g.Key.ToString(),
                OrderCount = g.Count(),
                Revenue = g.Sum(x => x.TotalPrice)
            }).ToList()
        };
    }
}

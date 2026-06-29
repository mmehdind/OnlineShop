using OnlineShop.Admin.DTOs.Dashboard;

namespace OnlineShop.Admin.ViewModels.Dashboard;

public class SalesChartVm
{
    public ChartPeriod Period { get; set; }

    public List<string> Labels { get; set; } = [];

    // نمودار تعداد سفارش‌ها
    public List<int> OrderCounts { get; set; } = [];

    // نمودار مبلغ فروش
    public List<long> Revenues { get; set; } = [];
}
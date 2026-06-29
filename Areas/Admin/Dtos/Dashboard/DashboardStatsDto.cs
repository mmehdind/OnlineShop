namespace OnlineShop.Admin.DTOs.Dashboard;

public class DashboardStatsDto
{
    public int TotalProducts { get; set; }

    public int TotalCategories { get; set; }

    public int TotalUsers { get; set; }

    public int TodayOrders { get; set; }

    public int PendingOrders { get; set; }

    public int ProcessingOrders { get; set; }

    public long TotalRevenue { get; set; }

    public long MonthlyRevenue { get; set; }
}
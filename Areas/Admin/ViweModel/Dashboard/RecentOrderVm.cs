namespace OnlineShop.Admin.ViewModels.Dashboard;

public class RecentOrderVm
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public long TotalPrice { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
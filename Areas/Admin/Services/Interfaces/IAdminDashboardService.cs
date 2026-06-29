using OnlineShop.Admin.ViewModels.Dashboard;
using OnlineShop.Admin.DTOs.Dashboard;

namespace OnlineShop.Services.Interfaces;

public interface IAdminDashboardService
{
    Task<DashboardVm> GetDashboardAsync(ChartPeriod period);
}
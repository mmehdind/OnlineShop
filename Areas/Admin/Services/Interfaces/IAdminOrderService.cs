using OnlineShop.Admin.ViewModels.Order;
using OnlineShop.Admin.DTOs.Order;
using OnlineShop.Common.Queries;

namespace OnlineShop.Services.Interfaces;

public interface IAdminOrderService
{
    Task<OrderListAdminVm> GetListAsync(OrderQuery query);

    Task<OrderDetailsDto?> GetDetailsAsync(int id);

    Task ChangeStatusAsync(ChangeOrderStatusDto dto);
}
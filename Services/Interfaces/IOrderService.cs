using OnlineShop.DTOs.Order;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderFromCartAsync(CheckoutDto dto);

    Task<List<Order>> GetUserOrdersAsync();

    Task<Order?> GetOrderByIdAsync(int id);

    Task<int> GetOrderByStstusAsync(OrderStatus status);
}
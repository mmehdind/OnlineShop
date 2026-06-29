using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Context;
using OnlineShop.DTOs.Order;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public OrderService(AppDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> CreateOrderFromCartAsync(CheckoutDto dto)
    {
        var userId = _currentUser.UserId;

        var cart = await _context.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.AppUserId == userId);

        if (cart == null || !cart.Items.Any())
            throw new Exception("Cart is empty");

        var address = await _context.Addresses
            .FirstOrDefaultAsync(x => x.Id == dto.AddressId && x.AppUserId == userId);

        if (address == null)
            throw new Exception("Address not found");

        var order = new Order
        {
            AppUserId = userId,
            AddressId = address.Id,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>()
        };

        long total = 0;

        foreach (var item in cart.Items)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            });

            total += item.Price * item.Quantity;
        }

        order.TotalPrice = total;

        _context.Orders.Add(order);

        _context.CartItems.RemoveRange(cart.Items);

        await _context.SaveChangesAsync();

        var payment = new Payment
        {
            OrderId = order.Id,
            Amount = order.TotalPrice,
            Status = PaymentStatus.Pending
        };

        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        return order.Id;
    }

    public async Task<List<Order>> GetUserOrdersAsync()
    {
        var userId = _currentUser.UserId;

        return await _context.Orders
            .Include(x => x.Items)
                .ThenInclude(x => x.Product)
            .Where(x => x.AppUserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        var userId = _currentUser.UserId;

        return await _context.Orders
            .Include(x => x.Items)
                .ThenInclude(x => x.Product)
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUserId == userId);
    }

    public async Task<int> GetOrderByStstusAsync(OrderStatus status)
    {
        return await _context.Orders.Where(x => x.Status == status).CountAsync();
    }
}
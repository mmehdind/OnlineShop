using Microsoft.EntityFrameworkCore;
using OnlineShop.Admin.ViewModels.Order;
using OnlineShop.Common.Queries;
using OnlineShop.Data.Context;
using OnlineShop.Services.Interfaces;
using OnlineShop.Admin.DTOs.Order;
using OnlineShop.Models;
using OnlineShop.Common;

namespace OnlineShop.Services.Implementations;

public class AdminOrderService : IAdminOrderService
{
    private readonly AppDbContext _context;

    public AdminOrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderListAdminVm> GetListAsync(OrderQuery query)
    {
        var ordersQuery = _context.Orders
            .Include(x => x.AppUser)
            .AsQueryable();

        // STATUS FILTER
        if (query.Status.HasValue)
        {
            ordersQuery = ordersQuery
                .Where(x => x.Status == query.Status.Value);
        }

        // SEARCH (UserName OR OrderId)
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();

            ordersQuery = ordersQuery.Where(x =>
                x.AppUser.UserName!.Contains(search) ||
                x.Id.ToString().Contains(search)
            );
        }

        // DATE FILTER (FROM)
        if (query.FromDate.HasValue)
        {
            var from = query.FromDate.Value.Date;

            ordersQuery = ordersQuery
                .Where(x => x.CreatedAt >= from);
        }

        // DATE FILTER (TO)
        if (query.ToDate.HasValue)
        {
            var to = query.ToDate.Value.Date.AddDays(1);

            ordersQuery = ordersQuery
                .Where(x => x.CreatedAt < to);
        }

        // SORT (newest first)
        ordersQuery = ordersQuery
            .OrderByDescending(x => x.CreatedAt);

        var orders = await ordersQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new OrderListAdminVm
        {
            StatusFilter = query.Status,

            Items = orders.Select(x => new OrderItemVm
            {
                Id = x.Id,
                UserName = x.AppUser.UserName ?? "Unknown",
                TotalPrice = x.TotalPrice,
                Status = x.Status.ToString()
            }).ToList()
        };
    }

    public async Task<OrderDetailsDto?> GetDetailsAsync(int id)
    {
        var order = await _context.Orders
            .Include(x => x.AppUser)
            .Include(x => x.Address)
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
            return null;

        return new OrderDetailsDto
        {
            Id = order.Id,
            UserName = order.AppUser.UserName ?? "Unknown",
            UserEmail = order.AppUser.Email ?? "",
            Status = order.Status.ToString(),
            TotalPrice = order.TotalPrice,
            Address =
                $"{order.Address.Province} - {order.Address.City} - {order.Address.Street}",
            CreatedAt = order.CreatedAt,

            Items = order.Items.Select(i => new OrderItemDetailDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }

    public async Task ChangeStatusAsync(ChangeOrderStatusDto dto)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == dto.OrderId);

        if (order == null)
            throw new Exception("Order not found");

        if (!OrderStatusRules.CanChange(order.Status, dto.Status))
            throw new Exception("Invalid status transition");

        order.Status = dto.Status;

        _context.OrderStatusHistory.Add(new OrderStatusHistory
        {
            OrderId = order.Id,
            Status = dto.Status
        });

        await _context.SaveChangesAsync();
    }
}
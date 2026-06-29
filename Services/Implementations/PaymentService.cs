using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Context;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.DTOs.Payment;

namespace OnlineShop.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreatePaymentAsync(int orderId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
            throw new Exception("Order not found");

        var payment = new Payment
        {
            OrderId = order.Id,
            Amount = order.TotalPrice,
            Status = PaymentStatus.Pending
        };

        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        return payment.Id;
    }

    public async Task<PaymentDto?> GetByIdAsync(int paymentId)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == paymentId);

        if (payment == null)
            return null;

        return new PaymentDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Status = payment.Status,
            TransactionId = payment.TransactionId,
            PaidAt = payment.PaidAt
        };
    }

    public async Task<PaymentDto?> GetByOrderIdAsync(int orderId)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(x => x.OrderId == orderId);

        if (payment == null)
            return null;

        return new PaymentDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Status = payment.Status,
            TransactionId = payment.TransactionId,
            PaidAt = payment.PaidAt
        };
    }

    public async Task<bool> ConfirmPaymentAsync(
        int paymentId,
        string transactionId)
    {
        var payment = await _context.Payments
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == paymentId);

        if (payment == null)
            return false;

        payment.Status = PaymentStatus.Success;
        payment.TransactionId = transactionId;
        payment.PaidAt = DateTime.UtcNow;

        payment.Order.Status = OrderStatus.Paid;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task FailPaymentAsync(int paymentId)
    {
        var payment = await _context.Payments
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == paymentId);

        if (payment == null)
            return;

        payment.Status = PaymentStatus.Failed;

        await _context.SaveChangesAsync();
    }
}
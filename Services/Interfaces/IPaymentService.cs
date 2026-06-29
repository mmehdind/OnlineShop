using OnlineShop.DTOs.Payment;

namespace OnlineShop.Services.Interfaces;

public interface IPaymentService
{
    Task<int> CreatePaymentAsync(int orderId);

    Task<PaymentDto?> GetByOrderIdAsync(int orderId);

    Task<PaymentDto?> GetByIdAsync(int paymentId);

    Task<bool> ConfirmPaymentAsync(int paymentId, string transactionId);

    Task FailPaymentAsync(int paymentId);
}
using OnlineShop.Models;

namespace OnlineShop.DTOs.Payment;

public class PaymentDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public long Amount { get; set; }

    public PaymentStatus Status { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? PaidAt { get; set; }
}
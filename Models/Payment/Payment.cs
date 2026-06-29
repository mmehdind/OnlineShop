using OnlineShop.Models.Base;

namespace OnlineShop.Models;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public long Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public PaymentMethod Method { get; set; } = PaymentMethod.OnlineGateway;

    public string? TransactionId { get; set; }

    public DateTime? PaidAt { get; set; }
}
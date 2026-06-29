namespace OnlineShop.ViewModels.Payment;

public class PaymentResultVm
{
    public bool IsSuccess { get; set; }

    public int OrderId { get; set; }

    public long Amount { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? TransactionId { get; set; }
}
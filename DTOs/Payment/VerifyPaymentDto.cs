namespace OnlineShop.DTOs.Payment;

public class VerifyPaymentDto
{
    public int PaymentId { get; set; }

    public string TransactionId { get; set; } = string.Empty;
}
namespace OnlineShop.DTOs.Cart;

public class CartActionResultDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public int CartItemsCount { get; set; }
}
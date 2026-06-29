using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces;

public interface ICartService
{
    Task<bool> AddToCartAsync(int productId, int quantity = 1);

    Task RemoveFromCartAsync(int productId);

    Task IncreaseAsync(int productId);

    Task DecreaseAsync(int productId);

    Task ClearAsync();

    Task<Cart?> GetUserCartAsync();

    Task<int> GetCartCountAsync();
}
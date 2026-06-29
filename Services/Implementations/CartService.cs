using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Data.Context;

namespace OnlineShop.Services.Implementations;

public class CartService : ICartService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private string UserId => _httpContextAccessor.HttpContext!.User.FindFirst("Id")!.Value;

    public CartService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private async Task<Cart> GetOrCreateCartAsync()
    {
        var cart = await _context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.AppUserId == UserId);

        if (cart != null)
            return cart;

        cart = new Cart
        {
            AppUserId = UserId
        };

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<bool> AddToCartAsync(int productId, int quantity = 1)
    {
        var cart = await GetOrCreateCartAsync();

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (product == null)
            return false;

        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = product.Id,
                Quantity = quantity,
                Price = product.Price
            });
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cart = await GetOrCreateCartAsync();

        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task IncreaseAsync(int productId)
    {
        var cart = await GetOrCreateCartAsync();

        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
            item.Quantity++;

        await _context.SaveChangesAsync();
    }

    public async Task DecreaseAsync(int productId)
    {
        var cart = await GetOrCreateCartAsync();

        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item == null)
            return;

        item.Quantity--;

        if (item.Quantity <= 0)
            cart.Items.Remove(item);

        await _context.SaveChangesAsync();
    }

    public async Task ClearAsync()
    {
        var cart = await GetOrCreateCartAsync();

        _context.CartItems.RemoveRange(cart.Items);

        await _context.SaveChangesAsync();
    }

    public async Task<Cart?> GetUserCartAsync()
    {

        var cart = await _context.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.AppUserId == UserId);

        return cart;
    }

    public async Task<int> GetCartCountAsync()
    {
        var cart = await GetUserCartAsync();

        if (cart == null)
        {
            return 0;        }

        return cart.Items.Sum(x => x.Quantity);
    }
}

    
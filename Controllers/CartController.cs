using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Controllers;

[Route("cart")]
public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(int productId, int quantity = 1)
    {
        var result = await _cartService.AddToCartAsync(productId, quantity);

        if (!result)
            return BadRequest(result);

        return Ok(result);
    }
}
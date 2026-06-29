using Microsoft.AspNetCore.Http;

namespace OnlineShop.Services.Interfaces;

public interface IProductImageService
{
    Task AddImageAsync(int productId, IFormFile file);

    Task DeleteImageAsync(int imageId);

    Task SetMainAsync(int imageId);
}
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Context;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class ProductImageService : IProductImageService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public ProductImageService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task AddImageAsync(int productId, IFormFile file)
    {
        var product = await _context.Products
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (product == null)
            throw new Exception("Product not found");

        if (!_fileService.IsImage(file))
            throw new Exception("Invalid file");

        var url = await _fileService.UploadAsync(file, "products");

        var image = new ProductImage
        {
            ProductId = productId,
            ImageUrl = url,
            IsMain = !product.Images.Any()
        };

        _context.ProductImages.Add(image);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteImageAsync(int imageId)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(x => x.Id == imageId);

        if (image == null)
            return;

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync();
    }

    public async Task SetMainAsync(int imageId)
    {
        var image = await _context.ProductImages
            .Include(x => x.Product)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(x => x.Id == imageId);

        if (image == null)
            return;

        foreach (var img in image.Product.Images)
        {
            img.IsMain = false;
        }

        image.IsMain = true;

        await _context.SaveChangesAsync();
    }
}
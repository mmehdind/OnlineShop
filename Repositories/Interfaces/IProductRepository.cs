using OnlineShop.Models;
using OnlineShop.Common;
using OnlineShop.Common.Queries;

namespace OnlineShop.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetDetailsAsync(int id, string? slug);

    Task<List<Product>> GetAllWithCategoryAsync();

    Task<PagedResult<Product>> GetPagedAsync(ProductQuery query);
}
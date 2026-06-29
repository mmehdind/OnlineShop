using OnlineShop.DTOs.Product;
using OnlineShop.Admin.DTOs.Product;
using OnlineShop.Common;
using OnlineShop.Common.Queries;

namespace OnlineShop.Services.Interfaces;

public interface IProductService
{
    Task CreateAsync(CreateProductAdminDto dto);

    Task<List<ProductDto>> GetAllAsync();

    Task<PagedResult<ProductDto>> GetPagedAsync(ProductQuery query);

    Task<ProductDto?> GetDetailsAsync(int id, string? slug);

     Task<List<ProductDto>> GetRelatedAsync(int categoryId, int currentProductId);

     Task<ProductDto?> GetByIdAsync(int id);

    Task UpdateAsync(UpdateProductDto dto);

    Task DeleteAsync(int id);
}
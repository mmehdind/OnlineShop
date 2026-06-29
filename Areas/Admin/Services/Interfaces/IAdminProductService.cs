using OnlineShop.Admin.ViewModels.Product;
using OnlineShop.Common.Queries;
using OnlineShop.Admin.DTOs.Product;

namespace OnlineShop.Services.Interfaces;

public interface IAdminProductService
{
    Task<ProductListAdminVm> GetListAsync(ProductQuery query);

    Task<ProductCreateVm> GetCreateVmAsync();
    Task CreateAsync(ProductCreateVm vm);

    Task<UpdateProductVm?> GetEditVmAsync(int id);
    Task UpdateAsync(UpdateProductVm vm);

    Task DeleteAsync(int id);

    Task<ProductAdminVm?> GetDetailsAsync(int id);

    Task AddImageAsync(int productId, IFormFile file);
    Task DeleteImageAsync(int imageId);
    Task SetMainImageAsync(int imageId);
}
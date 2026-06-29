using OnlineShop.ViewModels.Product;

namespace OnlineShop.Admin.ViewModels.Product;

public class UpdateProductVm : ProductBaseVm
{
    public int Id { get; set; }

    public List<ProductImageAdminVm> ProductImages { get; set; } = [];
}
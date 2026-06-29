using OnlineShop.ViewModels.Category;

namespace OnlineShop.ViewModels.Admin.Product;

public class ProductCategoryAdminVm
{
    public List<CategoryVm> Categories { get; set; } = [];

    public CreateCategoryVm CreateCategory { get; set; } = new();
}
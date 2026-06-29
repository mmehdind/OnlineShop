using OnlineShop.Common;
using OnlineShop.Common.Queries;
using OnlineShop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Admin.ViewModels.Product;

public class ProductListAdminVm
{
    public PagedResult<ProductItemVm> Products { get; set; } = null!;

    public ProductQuery Query { get; set; } = null!;

    public IEnumerable<SelectListItem> Categories { get; set; } = [];
}
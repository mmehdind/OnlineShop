using OnlineShop.Common;
using OnlineShop.Common.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.ViewModels.Product;

public class ProductListVm
{
    public PagedResult<ProductItemVm> Products { get; set; } = null!;

    public ProductQuery Query { get; set; } = null!;

    public IEnumerable<SelectListItem> Categories { get; set; } = [];
}
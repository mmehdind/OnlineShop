namespace OnlineShop.Admin.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductBaseVm
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public List<IFormFile> Images { get; set; } = [];

    public IEnumerable<SelectListItem> Categories { get; set; }
        = Enumerable.Empty<SelectListItem>();
}
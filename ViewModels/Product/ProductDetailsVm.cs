namespace OnlineShop.ViewModels.Product;

public class ProductDetailsVm
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long Price { get; set; }

    public int Stock { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<ProductImageVm> Images { get; set; } = [];
}
namespace OnlineShop.ViewModels.Product;

public class ProductItemVm
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public long Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public int Stock { get; set; }
}
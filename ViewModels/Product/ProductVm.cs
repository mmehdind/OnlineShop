namespace OnlineShop.ViewModels.Product;

public class ProductVm
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public long Price { get; set; }

    public string? ImageUrl { get; set; }

    public string? Slug { get; set; }
}
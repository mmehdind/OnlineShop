namespace OnlineShop.ViewModels.Product;

public class ProductImageVm
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsMain { get; set; }
}
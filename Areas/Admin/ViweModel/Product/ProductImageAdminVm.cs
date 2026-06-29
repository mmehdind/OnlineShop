namespace OnlineShop.Admin.ViewModels.Product;

public class ProductImageAdminVm
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsMain { get; set; }
}
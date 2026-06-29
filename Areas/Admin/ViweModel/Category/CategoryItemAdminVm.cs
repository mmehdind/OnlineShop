namespace OnlineShop.Admin.ViewModels.Category;

public class CategoryItemAdminVm
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public int ProductCount { get; set; } // خیلی مهم برای admin
}

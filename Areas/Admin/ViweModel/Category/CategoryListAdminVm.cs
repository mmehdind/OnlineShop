namespace OnlineShop.Admin.ViewModels.Category;

public class CategoryListAdminVm
{
    public List<CategoryItemAdminVm> Items { get; set; } = [];

    public int TotalProducts { get; set; } // optional (اگر خواستی حرفه‌ای‌تر بشه)
}

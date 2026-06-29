namespace OnlineShop.Admin.DTOs.Category;

public class CategoryListAdminDto
{
    public List<CategoryAdminDto> Items { get; set; } = [];

    public int TotalProducts { get; set; }
}

using OnlineShop.Models.Base;

namespace OnlineShop.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public bool IsDelete { get; set; } = false;

    public ICollection<Product> Products { get; set; } = [];
}

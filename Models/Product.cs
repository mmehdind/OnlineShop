using OnlineShop.Models.Base;

namespace OnlineShop.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long Price { get; set; }

    public int Stock { get; set; }

    public bool IsActive { get; set;} = true;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<ProductImage> Images { get; set; } = [];
}
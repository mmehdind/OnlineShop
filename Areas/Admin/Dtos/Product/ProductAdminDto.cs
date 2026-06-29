namespace OnlineShop.Admin.DTOs.Product;

public class ProductAdminDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long Price { get; set; }

    public int Stock { get; set; }

    public bool IsActive { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<ProductImageAdminDto> Images { get; set; } = [];
}
namespace OnlineShop.Admin.DTOs.Product;

public class CreateProductAdminDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public List<IFormFile> Images { get; set; } = [];
}
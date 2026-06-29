using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Admin.ViewModels.Category;

public class CategoryBaseVm
{
    [Required]
    [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Slug { get; set; } = string.Empty;
}
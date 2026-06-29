using OnlineShop.DTOs.Category;
using OnlineShop.Admin.DTOs.Category;

namespace OnlineShop.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();

    Task<CategoryListAdminDto> GetAllWithProductForAdminAsync();

    Task<CategoryDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateCategoryAdminDto dto);

    Task<bool> UpdateAsync(UpdateCategoryDto dto);

    Task<bool> DeleteAsync(int id);
}
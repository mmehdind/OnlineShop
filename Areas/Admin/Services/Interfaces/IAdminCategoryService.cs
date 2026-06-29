using OnlineShop.Admin.ViewModels.Category;

namespace OnlineShop.Services.Interfaces;

public interface IAdminCategoryService
{
    Task<CategoryListAdminVm> GetListAsync();

    Task<CreateCategoryAdminVm> GetCreateVmAsync();

    Task CreateAsync(CreateCategoryAdminVm vm);

    Task<int> GetCountAsync();

    Task<UpdateCategoryAdminVm?> GetEditVmAsync(int id);

    Task UpdateAsync(UpdateCategoryAdminVm vm);

    Task SoftDeleteAsync(int id);

    Task HardDeleteAsync(int id);
}
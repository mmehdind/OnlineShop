using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Admin.ViewModels.Category;
using OnlineShop.Data.Context;
using OnlineShop.Admin.DTOs.Category;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class AdminCategoryService : IAdminCategoryService
{
    private readonly AppDbContext _context;
    private readonly ICategoryService _catgoryService;
    private readonly IMapper _mapper;

    public AdminCategoryService(AppDbContext context, IMapper mapper, ICategoryService categoryService)
    {
        _context = context;
        _mapper = mapper;
        _catgoryService = categoryService;
    }

    public async Task<CategoryListAdminVm> GetListAsync()
    {
        var categories = await _catgoryService.GetAllWithProductForAdminAsync();

        return new CategoryListAdminVm
        {
            Items = categories.Items.Select(x => new CategoryItemAdminVm
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ProductCount = x.ProductCount
            }).ToList(),
            TotalProducts = categories.Items.Sum(x => x.ProductCount)
        };
    }

    public async Task<CreateCategoryAdminVm> GetCreateVmAsync()
        => new();

    public async Task CreateAsync(CreateCategoryAdminVm vm)
    {

        var category = _mapper.Map<CreateCategoryAdminDto>(vm);

        await _catgoryService.CreateAsync(category);
    }

    public async Task<UpdateCategoryAdminVm?> GetEditVmAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return null;

        return new UpdateCategoryAdminVm
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug
        };
    }

    public async Task UpdateAsync(UpdateCategoryAdminVm vm)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == vm.Id);

        if (category == null)
            throw new Exception("Category not found");

        category.Name = vm.Name;
        category.Slug = vm.Slug;

        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return;

        category.IsDelete = true;

        await _context.SaveChangesAsync();
    }

    public async Task HardDeleteAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Categories.CountAsync();
    }
}
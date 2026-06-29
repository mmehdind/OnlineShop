using AutoMapper;
using OnlineShop.DTOs.Category;
using OnlineShop.Models;
using OnlineShop.Repositories.Interfaces;
using OnlineShop.Services.Interfaces;
using OnlineShop.Exceptions;
using OnlineShop.Admin.DTOs.Category;

namespace OnlineShop.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(
    IRepository<Category> repo,
    ICategoryRepository categoryRepo,
    IUnitOfWork unitOfWork,
    IMapper mapper)
{
    _repo = repo;
    _categoryRepo = categoryRepo;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
}

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryListAdminDto> GetAllWithProductForAdminAsync()
    {
        var categories = await _categoryRepo.GetAllWithProductAsync();

        return new CategoryListAdminDto
        {
            Items = categories.Select(x => new CategoryAdminDto
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ProductCount = x.Products.Count
            }).ToList(),
            TotalProducts = categories.Sum(x => x.Products.Count)
        };
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task CreateAsync(CreateCategoryAdminDto dto)
    {
        var slugExists = await _repo
        .ExistsAsync(x => x.Slug == dto.Slug);

        if (slugExists)
        {
            throw new BusinessException(
                "Category slug already exists.");
        }

        var category = _mapper.Map<Category>(dto);

        await _repo.AddAsync(category);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _repo.GetByIdAsync(dto.Id);

        if (category == null)
            return false;

        _mapper.Map(dto, category);

        category.UpdatedAt = DateTime.UtcNow;

        _repo.Update(category);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);

        if (category == null)
            return false;

        await _repo.DeleteAsync(category);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

}
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Admin.DTOs.Product;
using OnlineShop.Admin.ViewModels.Product;
using OnlineShop.Common.Queries;
using OnlineShop.Services.Interfaces;
using OnlineShop.ViewModels.Product;
using OnlineShop.DTOs.Product;

namespace OnlineShop.Services.Implementations;

public class AdminProductService : IAdminProductService
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IProductImageService _imageService;
    private readonly IMapper _mapper;

    public AdminProductService(
        IProductService productService,
        ICategoryService categoryService,
        IProductImageService imageService,
        IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _imageService = imageService;
        _mapper = mapper;
    }

    // =========================
    // LIST
    // =========================
    public async Task<ProductListAdminVm> GetListAsync(ProductQuery query)
    {
        var result = await _productService.GetPagedAsync(query);
        var categories = await _categoryService.GetAllAsync();

        return new ProductListAdminVm
        {
            Query = query,

            Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }),

            Products = new Common.PagedResult<ProductItemVm>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,

                Items = result.Items.Select(x => new ProductItemVm
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Slug = x.Slug,
                    CategoryName = x.CategoryName,
                    Stock = x.Stock,
                    ImageUrl = x.Images.FirstOrDefault(i => i.IsMain)?.ImageUrl ?? ""
                }).ToList()
            }
        };
    }

    // =========================
    // CREATE VM
    // =========================
    public async Task<ProductCreateVm> GetCreateVmAsync()
    {
        var categories = await _categoryService.GetAllAsync();

        return new ProductCreateVm
        {
            Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
        };
    }

    // =========================
    // CREATE
    // =========================
    public async Task CreateAsync(ProductCreateVm vm)
    {
        var dto = _mapper.Map<CreateProductAdminDto>(vm);

        if (dto.Images.Count == 0)
        {
            throw new Exception("هیچ فایلی دریافت نشد.");
        }

        await _productService.CreateAsync(dto);
    }

    // =========================
    // EDIT VM
    // =========================
    public async Task<UpdateProductVm?> GetEditVmAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        var categories = await _categoryService.GetAllAsync();

        if (product == null)
            return null;

        var images = _mapper.Map<List<ProductImageAdminVm>>(product.Images);

        return new UpdateProductVm
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            ProductImages = images,
            Categories = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
        };
    }

    // =========================
    // UPDATE
    // =========================
    public async Task UpdateAsync(UpdateProductVm vm)
    {
        // 
        Console.WriteLine("checkpoint Edit Admin service");
        // 
        var dto = _mapper.Map<UpdateProductDto>(vm);

        await _productService.UpdateAsync(dto);
    }

    // =========================
    // DELETE
    // =========================
    public async Task DeleteAsync(int id)
    {
        await _productService.DeleteAsync(id);
    }

    // =========================
    // DETAILS
    // =========================
    public async Task<ProductAdminVm?> GetDetailsAsync(int id)
    {
        var product = await _productService.GetDetailsAsync(id, null);

        if (product == null)
            return null;

        return _mapper.Map<ProductAdminVm>(product);
    }

    // =========================
    // IMAGES
    // =========================
    public async Task AddImageAsync(int productId, IFormFile file)
    {
        await _imageService.AddImageAsync(productId, file);
    }

    public async Task DeleteImageAsync(int imageId)
    {
        await _imageService.DeleteImageAsync(imageId);
    }

    public async Task SetMainImageAsync(int imageId)
    {
        await _imageService.SetMainAsync(imageId);
    }
}
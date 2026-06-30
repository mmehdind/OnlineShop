using AutoMapper;
using OnlineShop.DTOs.Product;
using OnlineShop.Exceptions;
using OnlineShop.Models;
using OnlineShop.Repositories.Interfaces;
using OnlineShop.Services.Interfaces;
using OnlineShop.Common;
using OnlineShop.Common.Queries;
using OnlineShop.Admin.DTOs.Product;

namespace OnlineShop.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repo;
    private readonly IRepository<Category> _categoryRepo;
    private readonly IProductRepository _productRepo;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISlugService _slugService;

    public ProductService(
        IRepository<Product> repo,
        IRepository<Category> categoryRepo,
        IProductRepository productRepo,
        IFileService fileService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ISlugService slugService)
    {
        _repo = repo;
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _slugService = slugService;
    }

    public async Task CreateAsync(CreateProductAdminDto dto)
    {
        var category =
            await _categoryRepo.GetByIdAsync(dto.CategoryId);

        if (category is null)
        {
            throw new BusinessException(
                "Category not found.");
        }

        var product =
            _mapper.Map<Product>(dto);

        foreach (var image in dto.Images)
        {
            if (!_fileService.IsImage(image))
            {
                throw new BusinessException(
                    "Invalid image format.");
            }

            if (!_fileService.IsValidSize(image, 5))
            {
                throw new BusinessException(
                    "Image size exceeds limit.");
            }

            var imageUrl =
                await _fileService.UploadAsync(
                    image,
                    "products");

            product.Images.Add(new ProductImage
            {
                ImageUrl = imageUrl,
                IsMain = !product.Images.Any()
            });
        }

        var baseSlug = _slugService.Generate(dto.Name);

        var slug = baseSlug;
        var counter = 1;

        while (await _repo.ExistsAsync(x => x.Slug == slug))
        {
            slug = $"{baseSlug}-{counter}";
            counter++;
        }

        product.Slug = slug;

        await _repo.AddAsync(product);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products =
            await _productRepo.GetAllWithCategoryAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetDetailsAsync(int id, string? slug)
    {
        var product =
            await _productRepo.GetDetailsAsync(id, slug);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<PagedResult<ProductDto>> GetPagedAsync(ProductQuery query)
    {
        var result =
            await _productRepo.GetPagedAsync(query);

        return new PagedResult<ProductDto>
        {
            Items = result.Items
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    CategoryName = x.Category.Name,

                    Images = x.Images
                        .Select(i => new ProductImageDto
                        {
                            Id = i.Id,
                            ImageUrl = i.ImageUrl,
                            IsMain = i.IsMain
                        })
                        .ToList()
                })
                .ToList(),

            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalItems = result.TotalItems
        };
    }

    public async Task<List<ProductDto>> GetRelatedAsync(int categoryId, int currentProductId)
    {
        var products = await GetAllAsync();

        return products
            .Where(x => x.CategoryId == categoryId && x.Id != currentProductId)
            .Take(8)
            .ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);

        return product == null
            ? null
            : _mapper.Map<ProductDto>(product);
    }

    public async Task UpdateAsync(UpdateProductDto dto)
    {
        // 
        Console.WriteLine("checkpoint Edit service");
        // 
        var product = await _repo.GetByIdAsync(dto.Id);

        if (product == null)
            throw new Exception("Product not found");

        foreach (var image in dto.Images)
        {
            if (!_fileService.IsImage(image))
            {
                throw new BusinessException(
                    "Invalid image format.");
            }

            if (!_fileService.IsValidSize(image, 5 - dto.ProductImages.Count()))
            {
                throw new BusinessException(
                    "Image size exceeds limit.");
            }

            var imageUrl =
                await _fileService.UploadAsync(
                    image,
                    "products");

            product.Images.Add(new ProductImage
            {
                ImageUrl = imageUrl,
                IsMain = !product.Images.Any()
            });
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.CategoryId = dto.CategoryId;

        _repo.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);

        if (product == null)
            return;

        product.IsDeleted = true;

        _repo.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }
}
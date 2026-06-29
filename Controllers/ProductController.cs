using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Product;
using OnlineShop.Services.Interfaces;
using AutoMapper;
using OnlineShop.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Common.Queries;
using OnlineShop.Common;


namespace OnlineShop.Controllers;

[Route("product")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
{
    _productService = productService;
    _categoryService = categoryService;
    _mapper = mapper;
}
    
    public async Task<IActionResult> Index(ProductQuery query)
    {

    var productsDto = await _productService.GetPagedAsync(query);

    var categories = await _categoryService.GetAllAsync();

    var productsVm = new ProductListVm
    {
        Query = query,

        Categories = categories.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = query.CategoryId == x.Id
        }),

        Products = new PagedResult<ProductItemVm>
        {
            PageNumber = productsDto.PageNumber,
            PageSize = productsDto.PageSize,
            TotalItems = productsDto.TotalItems,

            Items = productsDto.Items
                .Select(x => new ProductItemVm
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Slug = x.Slug,
                    CategoryName = x.CategoryName,
                    Stock = x.Stock,
                    ImageUrl = x.Images
                        .FirstOrDefault(i => i.IsMain)
                        ?.ImageUrl ?? "/images/no-image.png"
                })
                .ToList()
        }
    };

    return View(productsVm);
    }
    

    [HttpGet]
    [Route("product/{id:int}/{slug?}")]
    public async Task<IActionResult> Details(int id, string? slug)
    {
        var productDto = await _productService.GetDetailsAsync(id, slug);

        if (productDto == null)
            return NotFound();

        var related = await _productService.GetRelatedAsync(productDto.CategoryId, productDto.Id);

        var vm = _mapper.Map<ProductDetailsVm>(productDto);

        ViewBag.RelatedProducts = related;

        return View(vm);
    }
}
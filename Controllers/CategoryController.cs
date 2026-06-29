using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Category;
using OnlineShop.Services.Interfaces;
using OnlineShop.ViewModels.Category;
using AutoMapper;
using OnlineShop.Models;

public class CategoryController : Controller
{
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var categories = _mapper.Map<List<CategoryVm>>(await _service.GetAllAsync());

        return View(categories);
    }

}
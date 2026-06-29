using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Context;
using OnlineShop.Models;
using OnlineShop.Repositories.Interfaces;
using OnlineShop.Common;
using OnlineShop.Common.Queries;

namespace OnlineShop.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetDetailsAsync(int id, string? slug)
{
    var query = _context.Products
        .Include(x => x.Category)
        .Include(x => x.Images)
        .AsQueryable();

    query = query.Where(x => x.Id == id);

    if (!string.IsNullOrWhiteSpace(slug))
    {
        query = query.Where(x => x.Slug == slug);
    }

    return await query.FirstOrDefaultAsync();
}

    public async Task<List<Product>> GetAllWithCategoryAsync()
    {
        return await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Images)
            .ToListAsync();
    }

    public async Task<PagedResult<Product>> GetPagedAsync(ProductQuery query)
    {
        var productsQuery = _context.Products
            .Include(x => x.Category)
            .Include(x => x.Images)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            productsQuery = productsQuery.Where(x =>
                x.Name.Contains(query.Search));
        }

        if (query.CategoryId.HasValue)
        {
            productsQuery = productsQuery.Where(x =>
                x.CategoryId == query.CategoryId.Value);
        }

        productsQuery = query.Sort switch
        {
            "price_asc" =>
                productsQuery.OrderBy(x => x.Price),

            "price_desc" =>
                productsQuery.OrderByDescending(x => x.Price),

            "name" =>
                productsQuery.OrderBy(x => x.Name),

            _ =>
                productsQuery.OrderByDescending(x => x.Id)
        };

        var totalItems = await productsQuery.CountAsync();

        var products = await productsQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedResult<Product>
        {
            Items = products,
            PageNumber = query.Page,
            PageSize = query.PageSize,
            TotalItems = totalItems
        };
    }
}
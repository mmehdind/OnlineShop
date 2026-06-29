using OnlineShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Context;
using OnlineShop.Models;

namespace OnlineShop.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{

    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllWithProductAsync()
    {
        return await _context.Categories
                            .Include(x => x.Products)
                            .ToListAsync();
    }
}

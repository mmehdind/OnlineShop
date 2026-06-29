using OnlineShop.Models;

namespace OnlineShop.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllWithProductAsync();
}
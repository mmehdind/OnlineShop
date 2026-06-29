using OnlineShop.Models.Base;
using System.Linq.Expressions;

namespace OnlineShop.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task DeleteAsync(T entity);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<int> GetCountAsync();
}
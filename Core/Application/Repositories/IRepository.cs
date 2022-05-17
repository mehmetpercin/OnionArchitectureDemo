using Domain.Entities.Common;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(string id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<bool> RemoveByFilterAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> RemoveByIdAsync(string id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}

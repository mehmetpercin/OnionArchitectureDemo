using Domain.Entities.Common;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task<bool> RemoveByFilterAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> RemoveByIdAsync(string id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}

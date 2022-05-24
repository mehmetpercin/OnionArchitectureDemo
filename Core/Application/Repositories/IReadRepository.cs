using Domain.Entities.Common;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IReadRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(string id);
    }
}

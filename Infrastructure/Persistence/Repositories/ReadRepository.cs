using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly EfCoreDbContext _context;

        public ReadRepository(EfCoreDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();
        public IQueryable<TEntity> GetAll() => Table.AsNoTracking();

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Table.AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return await Table.AsNoTracking().Where(filter).ToListAsync();
        }
    }
}

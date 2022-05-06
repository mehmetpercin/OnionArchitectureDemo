using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Contexts;
using Persistence.Settings;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TEntity> _collection;
        public Repository(IOptions<DatabaseSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            var data = await _collection.FindAsync(filter);
            return await data.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            var data = await _collection.FindAsync(filter);
            return await data.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            var data = await _collection.FindAsync(filter);
            return await data.ToListAsync();
        }

        public async Task<bool> RemoveByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            var deletedDocument = await _collection.FindOneAndDeleteAsync(filter);
            return deletedDocument != null;
        }

        public async Task<bool> RemoveByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            var deletedDocument = await _collection.FindOneAndDeleteAsync(filter);
            return deletedDocument != null;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            var updatedDocument = await _collection.ReplaceOneAsync(filter, entity);
            return updatedDocument.IsModifiedCountAvailable;
        }
    }
}

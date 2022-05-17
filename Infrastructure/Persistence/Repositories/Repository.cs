using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
            entity.CreatedDate = DateTimeOffset.Now;
            entity.Creator = "MP";
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            var now = DateTimeOffset.Now;
            foreach (var entity in entities)
            {
                entity.CreatedDate = now;
                entity.Creator = "MP";
            }
            await _collection.InsertManyAsync(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
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
            var objectId = ObjectId.Parse(id);
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var deletedDocument = await _collection.FindOneAndDeleteAsync(filter);
            return deletedDocument != null;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTimeOffset.Now;
            entity.Modifier = "MP";
            var objectId = ObjectId.Parse(entity.Id);
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            await _collection.FindOneAndReplaceAsync(filter,entity);
            return entity;
        }
    }
}

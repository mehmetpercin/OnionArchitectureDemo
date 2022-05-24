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
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TEntity> _collection;
        public WriteRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.Creator = "MP";
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            var now = DateTimeOffset.UtcNow;
            foreach (var entity in entities)
            {
                entity.CreatedDate = now;
                entity.Creator = "MP";
            }
            await _collection.InsertManyAsync(entities);
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
            entity.ModifiedDate = DateTimeOffset.UtcNow;
            entity.Modifier = "MP";
            var objectId = ObjectId.Parse(entity.Id);
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            await _collection.FindOneAndReplaceAsync(filter,entity);
            return entity;
        }
    }
}

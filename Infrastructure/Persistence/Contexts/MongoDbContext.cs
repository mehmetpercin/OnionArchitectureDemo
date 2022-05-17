using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Settings;

namespace Persistence.Contexts
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.Trim());
        }
    }
}

using Application.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Persistence.Settings;

namespace Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IOptions<DatabaseSettings> settings) : base(settings)
        {

        }
    }
}

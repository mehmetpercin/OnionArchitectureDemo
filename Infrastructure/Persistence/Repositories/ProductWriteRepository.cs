using Application.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Persistence.Settings;

namespace Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(IOptions<DatabaseSettings> settings) : base(settings)
        {

        }
    }
}

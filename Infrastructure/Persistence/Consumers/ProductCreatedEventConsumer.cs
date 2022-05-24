using Domain.Entities;
using Domain.Events;
using MassTransit;
using Persistence.Contexts;

namespace Persistence.Consumers
{
    public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly EfCoreDbContext _dbContext;

        public ProductCreatedEventConsumer(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var newProduct = new Product
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Price = context.Message.Price,
                Stock = context.Message.Stock,
            };

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();
        }
    }
}

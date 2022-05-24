using Application.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Consumers;
using Persistence.Contexts;
using Persistence.Repositories;
namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EfCoreDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
            });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductCreatedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMq"), "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint("product-created-queue", e =>
                    {
                        e.ConfigureConsumer<ProductCreatedEventConsumer>(context);
                    });
                });
            });

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}

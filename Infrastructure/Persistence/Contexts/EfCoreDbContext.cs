using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.ModifiedDate = DateTimeOffset.UtcNow;
                        entity.Entity.Modifier = "MP";
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        entity.Entity.Creator = "MP";
                        break;
                    default:
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

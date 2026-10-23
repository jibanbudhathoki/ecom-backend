using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecom.Domain.Entities.Catalog;

namespace Ecom.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Product> Products { get; }
        DbSet<ProductVariant> ProductVariants { get; }
        DbSet<Collection> Collections { get; }
        DbSet<ProductCollection> ProductCollections { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

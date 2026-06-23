using Microsoft.EntityFrameworkCore;
using SmartStore.Domain.Entities;

namespace SmartStore.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

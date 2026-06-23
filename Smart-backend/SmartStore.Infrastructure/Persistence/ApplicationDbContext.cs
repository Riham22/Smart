using Microsoft.EntityFrameworkCore;
using SmartStore.Application.Common.Interfaces; // أضيفي هذا الـ using
using SmartStore.Domain.Entities;

namespace SmartStore.Infrastructure.Persistence;

// قمنا بإضافة : IApplicationDbContext هنا
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // إعدادات هندسية متقدمة لجداول قاعدة البيانات (Fluent API)

        // تفاصيل جدول المنتجات
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.SKU).IsUnique(); // منع تكرار الـ SKU
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            // تصفية تلقائية لمنع جلب البيانات المحذوفة مؤقتاً (Global Query Filter for Soft Delete)
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // تفاصيل جدول التصنيفات
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.HasQueryFilter(e => !e.IsDeleted);
        });
    }
}

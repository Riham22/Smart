using SmartStore.Domain.Common;

namespace SmartStore.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty; // رمز المخزن الفريد
    public string? Description { get; set; }
    public decimal Price { get; set; } // نوع دقيق للحسابات المالية
    public int StockQuantity { get; set; }

    // العلاقة: المنتج ينتمي لتصنيف واحد
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}

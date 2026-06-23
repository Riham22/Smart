using SmartStore.Domain.Common;

namespace SmartStore.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // العلاقة: التصنيف يحتوي على العديد من المنتجات
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

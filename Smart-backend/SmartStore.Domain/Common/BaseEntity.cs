namespace SmartStore.Domain.Common;

public abstract class BaseEntity
{
	public Guid Id { get; set; } = Guid.NewGuid(); // استخدام Guid للتميز والأمان
	public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	public string CreatedBy { get; set; } = "System";
	public DateTime? LastModifiedDate { get; set; }
	public string? LastModifiedBy { get; set; }
	public bool IsDeleted { get; set; } = false; // تفعيل الـ Soft Delete
}

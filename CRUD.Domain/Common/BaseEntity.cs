namespace CRUD.Domain.Common;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}

public abstract class BaseAuditableEntity : BaseEntity
{
    public string CreatedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string LastModifiedBy { get; set; }
}
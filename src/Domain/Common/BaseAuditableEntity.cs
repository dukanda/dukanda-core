namespace DukandaCore.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public new Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; }

    public Guid? CreatedById { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public Guid? LastModifiedById { get; set; }
}

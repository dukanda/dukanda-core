namespace DukandaCore.Domain.Entities
{
    public class Benefit : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid PackageId { get; set; }
        public Package Package { get; set; } = null!;
    }
}
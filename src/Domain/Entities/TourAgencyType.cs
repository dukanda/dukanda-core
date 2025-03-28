using DukandaCore.Domain.Common;

namespace DukandaCore.Domain.Entities
{
    public class TourAgencyType : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int DisplayOrder { get; set; }

        public ICollection<TourAgency> Agencies { get; set; } = new List<TourAgency>();
    }
}

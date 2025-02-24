namespace DukandaCore.Domain.Entities
{
    public class TouristAttraction : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool IsFeatured { get; set; }
        public int CityId { get; set; }

        public City City { get; set; } = null!;
        public ICollection<AttractionImage> Gallery { get; set; } = new List<AttractionImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
namespace DukandaCore.Domain.Entities
{
    public class TouristAttraction : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool IsFeatured { get; set; }
        public int CityId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public City City { get; set; } = null!;
        public ICollection<AttractionGallery> Gallery { get; set; } = new List<AttractionGallery>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
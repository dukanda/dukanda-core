using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Entities
{
    public class Tour : BaseAuditableEntity
    {
        public Guid AgencyId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CityId { get; set; }
        public bool IsFeatured { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public Guid? PublishedById { get; set; }
        public int TotalSlots { get; set; }
        public int AvailableSlots { get; set; }
        public bool IsFullyBooked => AvailableSlots <= 0;

        public City City { get; set; } = null!;
        public TourAgency Agency { get; set; } = null!;
        public User? PublishedBy { get; set; }
        public ICollection<TourItinerary> Itineraries { get; set; } = new List<TourItinerary>();
        public ICollection<Package> Packages { get; set; } = new List<Package>();
        public ICollection<TourType> TourTypes { get; set; } = new List<TourType>();
        public ICollection<TouristAttraction> Attractions { get; set; } = new List<TouristAttraction>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<TourGallery> Gallery { get; set; } = new List<TourGallery>();
    }
}
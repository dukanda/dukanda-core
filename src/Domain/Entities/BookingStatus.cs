namespace DukandaCore.Domain.Entities
{
    public class BookingStatus : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int DisplayOrder { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
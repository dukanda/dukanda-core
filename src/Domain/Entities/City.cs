namespace DukandaCore.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        
        // Novas propriedades para coordenadas
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<TouristAttraction> Attractions { get; set; } = new List<TouristAttraction>();
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
namespace DukandaCore.Domain.Entities
{
    public class BannerType : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int DisplayOrder { get; set; }

        public ICollection<Banner> Banners { get; set; } = new List<Banner>();
    }
}
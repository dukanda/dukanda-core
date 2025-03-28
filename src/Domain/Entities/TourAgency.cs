using System.ComponentModel.DataAnnotations;
using DukandaCore.Domain.Common;
using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Entities
{
    public class TourAgency
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string LogoUrl { get; set; } = null!;
        public bool IsVerified { get; set; }
        public int TourAgencyTypeId { get; set; }

        public User User { get; set; } = null!;
        public TourAgencyType TourAgencyType { get; set; } = null!;
        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
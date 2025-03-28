using DukandaCore.Domain.Entities;

public class AttractionGalleryDto
{
    public Guid Id { get; set; }
    public Guid TouristAttractionId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Caption { get; set; } = null!;
    public int DisplayOrder { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }

    public AttractionGalleryDto(AttractionGallery gallery)
    {
        Id = Id;
        TouristAttractionId = TouristAttractionId;
        ImageUrl = ImageUrl;
        Caption = Caption;
        DisplayOrder = DisplayOrder;
        Created = Created;
        LastModified = LastModified;
    }
}

public class AttractionImageDto
{
    public Guid Id { get; set; }
    public Guid TouristAttractionId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}

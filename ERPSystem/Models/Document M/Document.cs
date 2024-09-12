namespace ERPSystem.Models.Document_M
{
    public class Document : BaseEntity, IAuditable
    {
        public string? DocumentName { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Content { get; set; }

        // Navigation properties
        public ICollection<DocVersion>? DocVersions { get; set; }
        public ICollection<AccessControl>? AccessControls { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

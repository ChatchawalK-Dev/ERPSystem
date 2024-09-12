namespace ERPSystem.Models.Document_M
{
    public class DocVersion : BaseEntity, IAuditable
    {
        public string? VersionNumber { get; set; }
        public DateTime ChangeDate { get; set; }
        public int DocumentID { get; set; }

        // Navigation property
        public Document? Document { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

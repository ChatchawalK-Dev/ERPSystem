namespace ERPSystem.Models.Document_M
{
    public class AccessControl : BaseEntity, IAuditable
    {
        public int UserID { get; set; }
        public int DocumentID { get; set; }
        public string? AccessLevel { get; set; }

        // Navigation property
        public Document? Document { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

namespace ERPSystem.Models.Manufacturing
{
    public class QualityControl : BaseEntity, IAuditable
    {
        public int ProductID { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }

        // Navigation property
        public Product? Product { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

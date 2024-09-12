namespace ERPSystem.Models.Project_M
{
    public class ResourceAllocation : BaseEntity, IAuditable
    {
        public int ResourceID { get; set; }
        public int Quantity { get; set; }
        public int ProjectID { get; set; }

        // Navigation property
        public Project? Project { get; set; }
        public Resource? Resource { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

namespace ERPSystem.Models.Project_M
{
    public class Resource : BaseEntity, IAuditable
    {
        public string? ResourceName { get; set; }
        public string? Type { get; set; }

        // Navigation property
        public ICollection<ResourceAllocation>? ResourceAllocations { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

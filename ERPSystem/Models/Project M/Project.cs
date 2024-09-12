using System.Reflection;

namespace ERPSystem.Models.Project_M
{
    public class Project : BaseEntity, IAuditable
    {
        public string? ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }

        // Navigation properties
        public ICollection<ProjectTask>? ProjectTasks { get; set; }
        public ICollection<ResourceAllocation>? ResourceAllocations { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

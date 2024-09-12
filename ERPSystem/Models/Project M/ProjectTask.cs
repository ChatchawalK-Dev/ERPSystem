namespace ERPSystem.Models.Project_M
{
    public class ProjectTask : BaseEntity, IAuditable
    {
        public string? TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public int ProjectID { get; set; }

        // Navigation property
        public Project? Project { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

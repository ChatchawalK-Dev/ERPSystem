using System;

namespace ERPSystem.Models.HumanResources
{
    public class Training : BaseEntity, IAuditable
    {
        public string? TrainingName { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public Employee? Employee { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

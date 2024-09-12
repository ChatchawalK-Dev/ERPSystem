using System;
using System.Collections.Generic;

namespace ERPSystem.Models.HumanResources
{
    public class Recruitment : BaseEntity, IAuditable
    {
        public string? Position { get; set; }
        public string? Status { get; set; }
        public DateTime Date { get; set; }

        // Navigation property
        public ICollection<Employee>? Employees { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

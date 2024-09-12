using System;
using System.Collections.Generic;

namespace ERPSystem.Models.HumanResources
{
    public class Employee : BaseEntity, IAuditable
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        public int RecruitmentID { get; set; }

        public Recruitment? Recruitment { get; set; }

        // Navigation properties
        public ICollection<Training>? Trainings { get; set; }
        public ICollection<TimeRecord>? TimeRecords { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

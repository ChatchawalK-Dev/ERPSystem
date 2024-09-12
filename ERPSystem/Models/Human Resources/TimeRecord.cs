using System;

namespace ERPSystem.Models.HumanResources
{
    public class TimeRecord : BaseEntity, IAuditable
    {
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public int EmployeeID { get; set; }

        // Navigation property
        public Employee? Employee { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

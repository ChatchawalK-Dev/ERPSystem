using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERPSystem.Models.Data_Analytics
{
    public class Dashboard : BaseEntity
    {
        public string DashboardName { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        // Navigation Property
        public ICollection<DataReport> DataReports { get; set; } = new HashSet<DataReport>();

    }
}

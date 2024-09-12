using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERPSystem.Models.Data_Analytics
{
    public class Analytics : BaseEntity
    {
        public string? AnalysisType { get; set; }
        public string? Data { get; set; }
        public DateTime Date { get; set; }

        // Navigation Property
        public ICollection<DataReport>? DataReports { get; set; }

    }
}

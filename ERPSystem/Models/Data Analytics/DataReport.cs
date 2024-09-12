using ERPSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPSystem.Models.Data_Analytics
{
    public class DataReport : BaseEntity
    {
        public string? ReportName { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Data { get; set; }

        // Foreign Key
        public int DashboardID { get; set; }

        // Navigation Property
        [ForeignKey(nameof(DashboardID))]
        public Dashboard? Dashboard { get; set; } = null!;

        // Foreign Key
        public int? AnalyticsID { get; set; }

        // Navigation Property
        [ForeignKey(nameof(AnalyticsID))]
        public Analytics? Analytics { get; set; }
    }
}

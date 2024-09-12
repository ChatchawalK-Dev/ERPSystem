using System;

namespace ERPSystem.Models.Finance
{
    public class Expense : BaseEntity, IAuditable
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public int BudgetID { get; set; }

        // Navigation property
        public Budget? Budget { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

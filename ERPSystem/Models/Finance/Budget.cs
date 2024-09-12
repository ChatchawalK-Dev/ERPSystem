using System;
using System.Collections.Generic;

namespace ERPSystem.Models.Finance
{
    public class Budget : BaseEntity, IAuditable
    {
        public string? BudgetName { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        public ICollection<Expense>? Expenses { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

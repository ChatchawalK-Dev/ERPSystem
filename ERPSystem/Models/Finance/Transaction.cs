using System;

namespace ERPSystem.Models.Finance
{
    public class Transaction : BaseEntity, IAuditable
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public int AccountID { get; set; }

        // Navigation property
        public Account? Account { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

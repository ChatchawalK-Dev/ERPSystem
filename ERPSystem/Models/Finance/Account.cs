using System;
using System.Collections.Generic;

namespace ERPSystem.Models.Finance
{
    public class Account : BaseEntity, IAuditable
    {
        public string? AccountName { get; set; }
        public decimal Balance { get; set; }

        // Navigation property
        public ICollection<Transaction>? Transactions { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

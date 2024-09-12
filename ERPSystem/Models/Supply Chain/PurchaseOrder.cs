using System;
using System.Collections.Generic;

namespace ERPSystem.Models.Supply_Chain
{
    public class PurchaseOrder : BaseEntity, IAuditable
    {
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation properties
        public ICollection<PurchaseOrderDetail>? PurchaseOrderDetails { get; set; }
        public ICollection<Delivery>? Deliveries { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

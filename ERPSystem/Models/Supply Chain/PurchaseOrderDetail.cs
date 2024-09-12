namespace ERPSystem.Models.Supply_Chain
{
    public class PurchaseOrderDetail : BaseEntity, IAuditable
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int PurchaseOrderID { get; set; }

        // Navigation property
        public PurchaseOrder? PurchaseOrder { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

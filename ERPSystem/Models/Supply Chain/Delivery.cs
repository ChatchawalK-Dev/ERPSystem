namespace ERPSystem.Models.Supply_Chain
{
    public class Delivery : BaseEntity, IAuditable
    {
        public DateTime DeliveryDate { get; set; }
        public string? Status { get; set; }
        public int PurchaseOrderID { get; set; }

        // Navigation property
        public PurchaseOrder? PurchaseOrder { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

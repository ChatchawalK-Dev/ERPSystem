namespace ERPSystem.Models.Cr
{
    public class SalesOrderDetail : BaseEntity, IAuditable
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int SalesOrderID { get; set; }

        // Navigation property
        public SalesOrder? SalesOrder { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

namespace ERPSystem.Models.Cr
{
    public class SalesOrder : BaseEntity, IAuditable
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerID { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public ICollection<SalesOrderDetail>? SalesOrderDetails { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

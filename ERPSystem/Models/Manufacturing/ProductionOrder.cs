namespace ERPSystem.Models.Manufacturing
{
    public class ProductionOrder :BaseEntity,IAuditable
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int ProductionPlanID { get; set; }

        // Navigation properties
        public ProductionPlan? ProductionPlan { get; set; }
        public Product? Product { get; set; }

        // IAuditable
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

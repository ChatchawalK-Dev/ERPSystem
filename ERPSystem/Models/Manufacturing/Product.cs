namespace ERPSystem.Models.Manufacturing
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public List<ProductionOrder>? ProductionOrders { get; set; }
        public Inventory? Inventory { get; set; }
        public List<QualityControl>? QualityControls { get; set; }

    }
}

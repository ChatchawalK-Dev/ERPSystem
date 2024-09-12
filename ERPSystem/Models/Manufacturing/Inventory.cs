using ERPSystem.Models.Supply_Chain;

namespace ERPSystem.Models.Manufacturing
{
    public class Inventory : BaseEntity
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public int WarehouseID { get; set; }
        // Navigation property
        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
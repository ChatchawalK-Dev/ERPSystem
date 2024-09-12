using ERPSystem.Models.Manufacturing;

namespace ERPSystem.Models.Supply_Chain
{
    public class Warehouse : BaseEntity, IAuditable
    {
        public string? Location { get; set; }
        public int Capacity { get; set; }

        // Navigation property
        public ICollection<Inventory>? Inventories { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

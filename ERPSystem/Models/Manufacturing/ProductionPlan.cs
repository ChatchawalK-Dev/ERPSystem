namespace ERPSystem.Models.Manufacturing
{
    public class ProductionPlan : BaseEntity, IAuditable
    {
        public string? PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        public List<ProductionOrder>? ProductionOrders { get; private set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public void AddOrder(ProductionOrder order)
        {
            if (order != null && ProductionOrders != null && !ProductionOrders.Contains(order))
            {
                ProductionOrders.Add(order);
            }
        }
    }
}

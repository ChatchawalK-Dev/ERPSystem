namespace ERPSystem.Models.Cr
{
    public class Customer : BaseEntity, IAuditable
    {
        public string? Name { get; set; }
        public string? ContactInfo { get; set; }
        public string? Address { get; set; }

        // Navigation property

        public int MarketingCampaignID { get; set; }
        public ICollection<SalesOrder>? SalesOrders { get; set; }
        public MarketingCampaign? MarketingCampaign { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

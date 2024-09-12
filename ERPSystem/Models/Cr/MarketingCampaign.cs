namespace ERPSystem.Models.Cr
{
    public class MarketingCampaign : BaseEntity, IAuditable
    {
        public string? CampaignName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        public ICollection<Customer>? Customers { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

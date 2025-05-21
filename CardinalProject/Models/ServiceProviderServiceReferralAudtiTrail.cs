namespace CardinalProject.Models
{
    public class ServiceProviderServiceReferralAuditTrail
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceProviderServiceReferralId { get; set; }
        public string DocumentName { get; set; }
        
        public bool IsApproved { get; set; }
        public int RequestType { get; set; }
        public DateTime RequestedAt { get; set; }
        public string RequestedBy { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string ApprovedBy { get; set; }
    }
}

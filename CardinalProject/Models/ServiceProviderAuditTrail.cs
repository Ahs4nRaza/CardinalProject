namespace CardinalProject.Models
{
    public class ServiceProviderAuditTrail
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string Website { get; set; }
        public bool IsApproved { get; set; }
        public int RequestType { get; set; }
        public DateTime RequestedAt { get; set; }
        public string RequestedBy { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string ApprovedBy { get; set; }
    }
}

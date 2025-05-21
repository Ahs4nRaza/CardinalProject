namespace CardinalProject.Models
{
    public class ServiceProviderServiceAuditTrail
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceProviderServiceId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceProviderName { get; set; }
        public string ServiceName { get; set; }
        public string HowToRefer { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Information { get; set; }
        public bool IsApproved { get; set; }
        public int RequestType { get; set; }
        public DateTime RequestedAt { get; set; }
        public string RequestedBy { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string ApprovedBy { get; set; }
        public int Section {  get; set; }
    }
}

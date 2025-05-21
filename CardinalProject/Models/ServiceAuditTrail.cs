namespace CardinalProject.Models
{
    public class ServiceAuditTrail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public bool IsApproved { get; set; }
        public int RequestType { get; set; }
        public DateTime RequestedAt { get; set; }
        public string RequestedBy { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string ApprovedBy { get; set; }
    }
}

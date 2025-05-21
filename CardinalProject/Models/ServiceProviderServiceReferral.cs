namespace CardinalProject.Models
{
    public class ServiceProviderServiceReferral
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        public string DocumentName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}

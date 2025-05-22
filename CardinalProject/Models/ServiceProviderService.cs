namespace CardinalProject.Models
{
    public class ServiceProviderService
    {
        public int Id { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        public string HowToRefer {  get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Information { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public ServiceProvider ServiceProvider { get; set; }
        public Service Service { get; set; }
    }
}

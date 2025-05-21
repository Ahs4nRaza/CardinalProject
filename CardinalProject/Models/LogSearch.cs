namespace CardinalProject.Models
{
    public class LogSearch
    {
        public int Id { get; set; }
        public string Postcode { get; set; }
        public string HospitalName { get; set; }
        public string ServiceName { get; set; }
        public string SearchedBy { get; set; }
        public DateTime SearchedAt { get; set; }
    }
}

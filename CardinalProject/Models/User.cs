namespace CardinalProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? HospitalId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string? Department {  get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        // to get names for hospital and role later
        public Hospital? Hospital { get; set; }
        public UserRole? Role { get; set; }
    }
}

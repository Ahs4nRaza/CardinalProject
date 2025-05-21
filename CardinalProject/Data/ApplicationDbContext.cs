using Microsoft.EntityFrameworkCore;
using CardinalProject.Constants;
using CardinalProject.Models;

namespace CardinalProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<LogSearch> LogSearches { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAuditTrail> ServiceAuditTrails { get; set; }
        public DbSet<CardinalProject.Models.ServiceProvider> ServiceProviders { get; set; }
        public DbSet<ServiceProviderAuditTrail> ServiceProviderAuditTrails { get; set; }
        public DbSet<ServiceProviderService> ServiceProviderServices { get; set; }
        public DbSet<ServiceProviderServiceAuditTrail> ServiceProviderServiceAuditTrails { get; set; }
        public DbSet<ServiceProviderServiceReferral> ServiceProviderServiceReferrals { get; set; }
        public DbSet<ServiceProviderServiceReferralAuditTrail> ServiceProviderServiceReferralAuditTrails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string todayString = "2025-05-21T00:00:00Z";
            base.OnModelCreating(modelBuilder);

            // Seed User Roles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Name = UserRoleConstants.User },
                new UserRole { Id = 2, Name = UserRoleConstants.DepartmentAdmin },
                new UserRole { Id = 3, Name = UserRoleConstants.HospitalAdmin },
                new UserRole { Id = 4, Name = UserRoleConstants.SuperAdmin }
            );

            // Seed Request Types
            modelBuilder.Entity<RequestType>().HasData(
                new RequestType { Id = 1, Name = RequestTypeConstants.Add },
                new RequestType { Id = 2, Name = RequestTypeConstants.Update },
                new RequestType { Id = 3, Name = RequestTypeConstants.Remove },
                new RequestType { Id = 4, Name = RequestTypeConstants.New }
            );

            // Seed Sections
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = 1, Name = SectionConstants.Service },
                new Section { Id = 2, Name = SectionConstants.Information }
            );

            // Seed SuperAdmin User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    RoleId = 4, // SuperAdmin
                    HospitalId = null,
                    Name = "System Administrator",
                    Email = "admin@cardinal.com",
                    PhoneNumber = "1234567890",
                    Department = null,
                    IsActive = true,
                    CreatedAt = DateTime.Parse(todayString),
                    UpdatedAt = DateTime.Parse(todayString),
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    PasswordHash = "$2a$12$bYNwbN2MMkAg8iE.J7Y2d.sGRY..z/9p/N/W2Z/YNxRMvws7ny98u"

                }
            );
        }
    }
}

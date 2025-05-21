using CardinalProject.Data;
using CardinalProject.Repositories;
using CardinalProject.Services;
using Microsoft.EntityFrameworkCore;

namespace CardinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
           

            var builder = WebApplication.CreateBuilder(args);

            // Load connection string from configuration (User Secrets, appsettings.json, etc.)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register ApplicationDbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add authentication with cookie scheme "UserAuth"
            builder.Services.AddAuthentication("UserAuth")
                .AddCookie("UserAuth", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout"; 
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.SlidingExpiration = true; // extends expiry time if user is active
                });

            builder.Services.AddAuthorization();

            // Add application services and repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add controllers with views support
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Important: Use Authentication before Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}

using api.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options)
        : base(options)
        {

        }

        public DbSet<CustomerRewards> CustomerRewards { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<RewardPoints> RewardPoints { get; set; }
        public DbSet<PointsTransaction> PointsTransactions { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorIntegration> VendorIntegrations { get; set; }
        public DbSet<VendorOrder> VendorOrders { get; set; }
        public DbSet<FavoriteOutlet> FavoriteOutlets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                   .HasIndex(u => u.UserCode)
                   .IsUnique();

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
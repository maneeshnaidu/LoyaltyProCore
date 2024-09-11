using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProCore.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options)
        : base(options)
        {

        }

        public DbSet<CustomerRewards> CustomerRewards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<PointsTransaction> PointsTransactions { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

    }
}
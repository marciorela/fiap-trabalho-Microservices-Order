using Geekburger.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Geekburger.Order.Database
{
    public class OrderDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<Domain.Entities.Product> OrdersProducts { get; set; }
        public DbSet<Domain.Entities.Production> OrdersProduction { get; set; }
        public DbSet<Domain.Entities.Payment> OrdersPayments { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderDbContext(IConfiguration config)
        {
            _config = config;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.GetConnectionString("OrderDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(s => new { s.OrderId, s.ProductId });
            modelBuilder.Entity<Production>().HasKey(s => new { s.OrderId, s.ProductionId });
            modelBuilder.Entity<Payment>().HasKey(s => new { s.OrderId, s.RequesterId });
        }
    }
}
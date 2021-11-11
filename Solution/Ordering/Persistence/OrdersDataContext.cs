using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrdersApi.Models;

namespace OrdersApi.Persistence
{
    public class OrdersDataContext : DbContext
    {
        public OrdersDataContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EnumToStringConverter<Status> converter = new();
            builder.Entity<Order>()
                .Property(prop => prop.Status)
                .HasConversion(converter);

            builder.Entity<Order>()
                .HasKey(prop => prop.OrderId);

            builder.Entity<OrderDetails>()
                .HasKey(prop => prop.OrderDetailId);

            builder.Entity<OrderDetails>()
                .HasOne<Order>(prop => prop.Order)
                .WithMany(prop => prop.OrderDetails)
                .HasForeignKey(prop => prop.OrderId);

            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
using Booking.Domain.Entities.Common;
using Booking.Domain.Entities.Customers;
using Booking.Domain.Entities.Dictionaries;
using Booking.Domain.Entities.Orders;
using Booking.Domain.Entities.Providers;
using Booking.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Ninject.Activation;
using StackExchange.Redis;
using System.Reflection;

namespace Booking.Infrastructure.Persistence
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider>().HasIndex(e => e.CategoryId).HasName("IX_Category_Id");
            modelBuilder.Entity<Order>().HasIndex(p => new { p.ProviderId, p.CustomerId });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

       

        #region Customer
        public DbSet<Customer>? Customers { get; set; }
        #endregion

        #region User
        public DbSet<User>? Users { get; set; }
        public DbSet<Domain.Entities.Users.Role>? Roles { get; set; }
        #endregion

    }
}

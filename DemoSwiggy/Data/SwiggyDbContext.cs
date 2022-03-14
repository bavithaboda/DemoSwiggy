using DemoSwiggy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Data
{
    public class SwiggyDbContext:DbContext
    {
        public SwiggyDbContext(DbContextOptions<SwiggyDbContext> options) :base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API's

            modelBuilder.Entity<Order>().Property(x => x.City).HasMaxLength(30);
            modelBuilder.Entity<Order>().HasKey(x => x.OrderId);

            modelBuilder.Entity<Order>()
                                           .HasOne(x => x.Customer)
                                           .WithMany(x => x.Orders)
                                           .HasForeignKey(x => x.CustomerId);

           

                
            // modelBuilder.Entity<CustomerOrders>().HasNoKey().ToView("vw_GetCustomerOrderDetailsView");
            //HasOne
            //Withone//WithMany
        }


        public virtual DbSet<Customer> customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Items> items { get; set; }
        DbSet<SupportEmployee> se { get; set; }
        DbSet<Organization> organizations { get; set; }
        DbSet<Address> addresses { get; set; }
    }
}


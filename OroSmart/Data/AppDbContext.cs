﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OroSmart.Models;

namespace OroSmart.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomersWorkLocation> CustomersWorkLocations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define foreign key relationships
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomersAdded)
                .WithMany(u => u.CustomersAdded)
                .HasForeignKey(c => c.first_entry_user_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomersLastUpdated)
                .WithMany(u => u.CustomersLastUpdated)
                .HasForeignKey(c => c.last_update_user_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomersWorkLocation>()
                .HasKey(cwl => cwl.Id);

            modelBuilder.Entity<CustomersWorkLocation>()
                .HasOne(cwl => cwl.Customer)
                .WithOne(c => c.WorkLocation)
                .HasForeignKey<CustomersWorkLocation>(cwl => cwl.CustomerId);
        }

    }
}

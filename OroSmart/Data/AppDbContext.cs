using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<CustomersContacts> CustomersContacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }



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
               .HasOne(cwl => cwl.ReferencePerson)
               .WithMany(c => c.WorkLocations)
               .HasForeignKey(cwl => cwl.ReferencePersonId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CustomersWorkLocation>()
                .HasOne(cwl => cwl.Customer)
                .WithOne(c => c.WorkLocation)
                .HasForeignKey<CustomersWorkLocation>(cwl => cwl.CustomerId);

            
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CustomersContacts)
                .WithOne(cc => cc.Customer) 
                .HasForeignKey(cc => cc.CustomerId) 
                .IsRequired(); 

            modelBuilder.Entity<CustomersContacts>()
                .HasOne(cc => cc.ContactType)
                .WithMany() 
                .HasForeignKey(cc => cc.ContactTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}

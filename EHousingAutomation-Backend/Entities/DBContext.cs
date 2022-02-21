using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities
{
    public class DBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Database = E-Housing; integrated security = True;");
        }

        //set model
        public DbSet<House> House { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Landlord> Landlord { get; set;}
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Dues> Dues { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<APIAuthority> APIAuthority { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>().ToTable("House");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Landlord>().ToTable("Landlord");
            modelBuilder.Entity<Bill>().ToTable("Bill");
            modelBuilder.Entity<Dues>().ToTable("Dues");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<APIAuthority>().ToTable("APIAuthority");
        }


    }
}

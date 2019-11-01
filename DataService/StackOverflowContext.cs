using Microsoft.EntityFrameworkCore;

namespace rawdata_portfolioproject_2
{
    public class StackOverflowContext : DbContext
    {
        // optional: make functions for making mapping easier... look at livecode6 for inspiration
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("host=localhost;db=northwind;uid=postgres;pwd=postgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // map relational model to object model like so:
            //modelBuilder.Entity<Category>().ToTable("categories");
            //modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            //modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
            //modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

            modelBuilder.Entity<Profile>().ToTable("profiles");
        }

        
    }
}
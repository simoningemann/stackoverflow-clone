using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2
{
    public class StackOverflowContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile_LoginResult> Profile_LoginResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder // remember to connect to vpn
                .UseNpgsql("Server=rawdata.ruc.dk;Port=5432;Database=raw11;User ID=raw11;Password=eMYte)i.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CreateMap(); // maps all entities and properties to their respective tables and columns
            
            //set composite primary keys
            modelBuilder.Entity<Link>().HasKey(x => new {x.PostId, x.LinkPostId});
            
            // query result mapping
            modelBuilder.Entity<Profile_LoginResult>().HasNoKey();
            modelBuilder.Entity<Profile_LoginResult>().Property(x => x.Result).HasColumnName("profile_login");
        }
    }
    static class ModelBuilderExtensions
    {
        public static void CreateMap(this ModelBuilder modelBuilder)
        {
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.GetTableName().ToLower());
                foreach(var property in entityType.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }

            }
        }
    }
}
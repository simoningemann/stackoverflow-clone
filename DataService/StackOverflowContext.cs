using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        public DbQuery<Ranked_Weight_VariadicResult> Ranked_Weight_VariadicResults {get; set;}
        public DbQuery<Profile_LoginResult> Profile_LoginResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder // remember to connect to vpn
                .UseNpgsql("Server=rawdata.ruc.dk;Port=5432;Database=raw11;User ID=raw11;Password=eMYte)i.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // map relational model to object model like so:
            //modelBuilder.Entity<Category>().ToTable("categories");
            //modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            //modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
            //modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");
            // or use the CreateMap function at the bottom
            
            modelBuilder.CreateMap(); // maps all entities and properties to their respective tables and columns
            
            // map the primary <attribute>id columns to their respective <Property>.Id property names (for convienience)
            modelBuilder.Entity<Profile>().Property(x => x.Id).HasColumnName("profileid");
            modelBuilder.Entity<Post>().Property(x => x.Id).HasColumnName("postid");
            modelBuilder.Entity<Post>().Ignore(x => x.Answer);
            modelBuilder.Entity<Post>().Ignore(x => x.Question);
            modelBuilder.Entity<Post>().Ignore(x => x.User);
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("postid");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("userid");

            //set composite primary keys
            modelBuilder.Entity<Bookmark>().HasKey(x => new {x.ProfileId, x.PostId});
            modelBuilder.Entity<Query>().HasKey(x => new {x.ProfileId, x.TimeSearched});
            modelBuilder.Entity<Link>().HasKey(x => new {x.PostId, x.LinkToPostId});
            
            // query result mapping
            modelBuilder.Entity<Profile_LoginResult>().Property(x => x.Result).HasColumnName("profile_login");
            modelBuilder.Entity<Ranked_Weight_VariadicResult>().Property(x => x.PostId).HasColumnName("pid");
            modelBuilder.Entity<Ranked_Weight_VariadicResult>().Property(x => x.Weight).HasColumnName("w");
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
using Homework_7_1.Models.Configurations;
using Homework_7_1.Models.Domains;
using Homework_7_1.Properties;
using System.Data.Entity;

namespace Homework_7_1
{
    public class ApplicationDbContext : DbContext
    {
        public static string _connectionString = 
            $@"Server={Settings.Default.ServerAddress}{Settings.Default.ServerName};
            Database={Settings.Default.DatabaseName};
            User Id={Settings.Default.DatabaseLogin};
            Password={Settings.Default.DatabasePassword}";
        public ApplicationDbContext()
            : base(_connectionString)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
        }
    }

}
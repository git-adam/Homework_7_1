using Homework_7_1.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Homework_7_1.Models.Configurations
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(x => x.Id);
        }
    }
}

using Homework_7_1.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Homework_7_1.Models.Configurations
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);
        }
    }
}

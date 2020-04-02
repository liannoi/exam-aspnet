using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(e => e.ActorId);
            builder.Property(e => e.Birthday).HasColumnType("date");
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(54);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(54);
        }
    }
}
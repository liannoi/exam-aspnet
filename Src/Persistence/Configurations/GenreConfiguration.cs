using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(e => e.GenreId);
            builder.HasIndex(e => e.Title).HasName("UNQ_Genres_Title").IsUnique();
            builder.Property(e => e.Title).HasMaxLength(64);
        }
    }
}
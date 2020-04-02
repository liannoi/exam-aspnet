using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(e => e.FilmId);
            builder.HasIndex(e => e.Title).HasName("UNQ_Films_Title").IsUnique();
            builder.Property(e => e.Description).HasMaxLength(4000);
            builder.Property(e => e.PublishYear).HasColumnType("date");
            builder.Property(e => e.Title).IsRequired().HasMaxLength(128);
        }
    }
}
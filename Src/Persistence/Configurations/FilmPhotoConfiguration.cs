using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class FilmPhotoConfiguration : IEntityTypeConfiguration<FilmPhoto>
    {
        public void Configure(EntityTypeBuilder<FilmPhoto> builder)
        {
            builder.HasKey(e => e.PhotoId);
            builder.HasIndex(e => e.Path).HasName("UNQ_FilmsPhotos_Path").IsUnique();
            builder.Property(e => e.Path).IsRequired().HasMaxLength(512);

            builder.HasOne(d => d.Film)
                .WithMany(p => p.FilmsPhotos)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FilmsPhotos_FilmId");
        }
    }
}
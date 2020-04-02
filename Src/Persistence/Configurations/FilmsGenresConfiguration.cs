using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class FilmsGenresConfiguration : IEntityTypeConfiguration<FilmsGenres>
    {
        public void Configure(EntityTypeBuilder<FilmsGenres> builder)
        {
            builder.HasKey(e => new {e.FilmId, e.GenreId});

            builder.HasOne(d => d.Film)
                .WithMany(p => p.FilmGenres)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FilmsGenres_FilmId");

            builder.HasOne(d => d.Genre)
                .WithMany(p => p.FilmsGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FilmsGenres_GenreId");
        }
    }
}
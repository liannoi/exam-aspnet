using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class ActorsFilmsConfiguration : IEntityTypeConfiguration<ActorsFilms>
    {
        public void Configure(EntityTypeBuilder<ActorsFilms> builder)
        {
            builder.HasKey(e => new {e.ActorId, e.FilmId});

            builder.HasOne(d => d.Actor)
                .WithMany(p => p.ActorsFilms)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActorsFilms_ActorId");

            builder.HasOne(d => d.Film)
                .WithMany(p => p.ActorFilms)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActorsFilms_FilmId");
        }
    }
}
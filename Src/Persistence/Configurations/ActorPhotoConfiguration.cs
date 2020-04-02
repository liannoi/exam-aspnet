using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class ActorPhotoConfiguration : IEntityTypeConfiguration<ActorPhoto>
    {
        public void Configure(EntityTypeBuilder<ActorPhoto> builder)
        {
            builder.HasKey(e => e.PhotoId);
            builder.HasIndex(e => e.Path).HasName("UNQ_ActorsPhotos_Path").IsUnique();
            builder.Property(e => e.Path).IsRequired().HasMaxLength(512);

            builder.HasOne(d => d.Actor)
                .WithMany(p => p.ActorsPhotos)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActorsPhotos_ActorId");
        }
    }
}
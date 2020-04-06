using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class VotingPollesConfiguration : IEntityTypeConfiguration<VotingPolleRelation>
    {
        public void Configure(EntityTypeBuilder<VotingPolleRelation> builder)
        {
            builder.HasKey(e => e.VotingPolleId);

            builder.HasOne(d => d.Polle)
                .WithMany(p => p.VotingPolles)
                .HasForeignKey(d => d.PolleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VotingPolles_PolleId");

            builder.HasOne(d => d.VotingAnswer)
                .WithMany(p => p.VotingPolles)
                .HasForeignKey(d => d.VotingAnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VotingPolles_VotingAnswerId");

            builder.HasOne(d => d.Voting)
                .WithMany(p => p.VotingPolles)
                .HasForeignKey(d => d.VotingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VotingPolles_VotingId");
        }
    }
}
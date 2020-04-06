using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Persistence.Configurations
{
    public class VotingPolleConfiguration : IEntityTypeConfiguration<VotingPolle>
    {
        public void Configure(EntityTypeBuilder<VotingPolle> builder)
        {
            builder.Property(e => e.VotingPolleId).ValueGeneratedNever();
        }
    }
}
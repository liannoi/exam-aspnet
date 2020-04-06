using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class VotingPolle
    {
        public VotingPolle()
        {
            VotingPolles = new HashSet<VotingPolleRelation>();
        }

        public int VotingPolleId { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public ICollection<VotingPolleRelation> VotingPolles { get; private set; }
    }
}
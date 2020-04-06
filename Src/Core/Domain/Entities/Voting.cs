using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Voting
    {
        public Voting()
        {
            VotingAnswers = new HashSet<VotingAnswer>();
            VotingPolles = new HashSet<VotingPolleRelation>();
        }

        public int VotingId { get; set; }
        public string Name { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public ICollection<VotingAnswer> VotingAnswers { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public ICollection<VotingPolleRelation> VotingPolles { get; private set; }
    }
}
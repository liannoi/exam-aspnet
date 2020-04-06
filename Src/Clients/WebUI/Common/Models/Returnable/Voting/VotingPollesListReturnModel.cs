using System.Collections.Generic;

namespace Exam.Clients.WebUI.Common.Models.Returnable.Voting
{
    public class VotingPollesListReturnModel
    {
        public IEnumerable<VotingPolleReturnModel> VotingPolles { get; set; }
    }
}
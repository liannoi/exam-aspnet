using System.Collections.Generic;
using Exam.Clients.WebUI.Common.Models.Returnable.Voting;

namespace Exam.Clients.WebUI.Common.Models.ViewModels
{
    public class VotingViewModel
    {
        public string Name { get; set; }
        public IEnumerable<VotingAnswerReturnModel> VotingAnswers { get; set; }
        public int VotingCount { get; set; }
    }
}
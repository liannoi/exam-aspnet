using System.Linq;
using System.Threading.Tasks;
using Exam.Clients.WebUI.Common.Helpers.ApiTools;
using Exam.Clients.WebUI.Common.Models.Returnable.Voting;
using Exam.Clients.WebUI.Common.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Clients.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiTools _apiTools;

        public HomeController()
        {
            _apiTools = new ApiTools();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new VotingViewModel
            {
                Name = (await _apiTools.FetchAsync<VotingReturnModel>("https://localhost:5001/api/voting/get/1")).Name,

                VotingAnswers =
                    (await _apiTools.FetchAsync<VotingAnswersListReturnModel>(
                        "https://localhost:5001/api/voting/getanswersbyvoting/1")).VotingAnswers,

                VotingCount = (await _apiTools.FetchAsync<VotingPollesListReturnModel>(
                        "https://localhost:5001/api/voting/getvotingpolles/1"))
                    .VotingPolles
                    .Select(x => x.PolleId)
                    .Distinct()
                    .Count()
            });
        }
    }
}
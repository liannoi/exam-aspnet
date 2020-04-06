using System.Threading.Tasks;
using Exam.Application.Storage.Voting;
using Exam.Application.Storage.Voting.Queries.Get;
using Exam.Application.Storage.Voting.Queries.Get.AsList;
using Exam.Application.Storage.VotingPolles.Commands.Create;
using Exam.Application.Storage.VotingPolles.Queries.Get;
using Exam.Application.Storage.VotingPolles.Queries.Get.AsList;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Clients.WebApi.Controllers
{
    public class VotingController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<VotingLookupDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetVotingQuery {VotingId = id}));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<VotingAnswersListViewModel>> GetAnswersByVoting(int id)
        {
            return Ok(await Mediator.Send(new GetVotingAnswersAsListQuery {VotingId = id}));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<VotingPollesListViewModel>> GetVotingPolles(int id)
        {
            return Ok(await Mediator.Send(new GetVotingPollesCountQuery {VotingId = id}));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVotingPolle(CreateVotingPolleCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
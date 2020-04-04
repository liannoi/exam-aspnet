using System.Threading.Tasks;
using Exam.Application.Storage.Films.Queries.Get.AsList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Clients.WebApi.Controllers
{
    public class FilmsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FilmsListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetFilmsAsListQuery()));
        }
    }
}
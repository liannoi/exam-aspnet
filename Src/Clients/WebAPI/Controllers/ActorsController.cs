using System;
using System.Threading.Tasks;
using Exam.Application.Storage.Actors;
using Exam.Application.Storage.Actors.Commands.Create;
using Exam.Application.Storage.Actors.Commands.Delete;
using Exam.Application.Storage.Actors.Commands.Update;
using Exam.Application.Storage.Actors.Queries.Get;
using Exam.Application.Storage.Actors.Queries.Get.AsList;
using Exam.Application.Storage.Actors.Queries.Get.AsList.ByFilm;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Clients.WebApi.Controllers
{
    public class ActorsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ActorsListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetActorsAsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ActorLookupDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetActorQuery {ActorId = id}));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ActorsListViewModel>> GetAllByFilm(int id)
        {
            return Ok(await Mediator.Send(new GetActorsByFilmAsListQuery {FilmId = id}));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateActorCommand command)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateActorCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("No entities with this primary key were found in the database.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteActorCommand {ActorId = id}));
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("No entities with this primary key were found in the database.");
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Exam.Application.Storage.Films;
using Exam.Application.Storage.Films.Commands.Create;
using Exam.Application.Storage.Films.Commands.Delete;
using Exam.Application.Storage.Films.Commands.Update;
using Exam.Application.Storage.Films.Queries.Get;
using Exam.Application.Storage.Films.Queries.Get.AsList;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Clients.WebApi.Controllers
{
    public class FilmsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<FilmsListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetFilmsAsListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<FilmLookupDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetFilmQuery {Id = id}));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateFilmCommand command)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateFilmCommand command)
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
                return Ok(await Mediator.Send(new DeleteFilmCommand {FilmId = id}));
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
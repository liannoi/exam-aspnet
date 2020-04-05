using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Clients.WebUI.Common.Helpers.ApiTools;
using Exam.Clients.WebUI.Common.Models.Returnable.Actor;
using Exam.Clients.WebUI.Common.Models.Returnable.Film;
using Exam.Clients.WebUI.Common.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exam.Clients.WebUI.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IApiTools _apiTools;

        public FilmsController()
        {
            // TODO: DI.
            _apiTools = new ApiTools();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // TODO: To local Consts.cs.

            return View((await _apiTools.FetchAsync<FilmsListReturnModel>("https://localhost:5001/api/films/getall"))
                .Films
                .TakeLast(10));
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(int id)
        {
            return View(await PrepareAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate([FromForm] CreateOrUpdateFilmViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await PrepareAsync(model);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: To local Consts.cs.
            await _apiTools.DeleteAsync($"https://localhost:5001/api/films/delete/{id}");

            return Ok();
        }

        #region Helpers

        private async Task<CreateOrUpdateFilmViewModel> PrepareAsync(int id)
        {
            FilmReturnModel film;
            IEnumerable<int> selectedActors = null;

            if (id == 0)
            {
                film = new FilmReturnModel();
            }
            else
            {
                film = await _apiTools.FetchAsync<FilmReturnModel>($"https://localhost:5001/api/films/get/{id}");
                selectedActors =
                    (await _apiTools.FetchAsync<ActorsListReturnModel>(
                        $"https://localhost:5001/api/actors/getallbyfilm/{id}")).Actors.Select(x => x.ActorId);
            }

            return new CreateOrUpdateFilmViewModel
            {
                Film = film,

                Actors = (await _apiTools.FetchAsync<ActorsListReturnModel>("https://localhost:5001/api/actors/getall"))
                    .Actors
                    .TakeLast(10)
                    .Select(x => new SelectListItem
                    {
                        Value = x.ActorId.ToString(),
                        Text = $"{x.FirstName} {x.LastName}"
                    }),

                SelectedActors = selectedActors
            };
        }

        private async Task<IEnumerable<ActorReturnModel>> FetchSelectedActorsAsync(CreateOrUpdateFilmViewModel model)
        {
            var actors = new List<ActorReturnModel>();
            foreach (var selectedActorId in model.SelectedActors)
                actors.Add(await _apiTools.FetchAsync<ActorReturnModel>(
                    $"https://localhost:5001/api/actors/get/{selectedActorId}"));

            return actors;
        }

        private async Task PrepareAsync(CreateOrUpdateFilmViewModel model)
        {
            var selectedActors = await FetchSelectedActorsAsync(model);

            if (model.Film.FilmId == 0)
                await _apiTools.PostAsync<FilmReturnModel>("https://localhost:5001/api/films/create",
                    new {model.Film.Title, model.Film.PublishYear, model.Film.Description, Actors = selectedActors});
            else
                await _apiTools.PostAsync<FilmReturnModel>("https://localhost:5001/api/films/update",
                    new
                    {
                        model.Film.FilmId,
                        model.Film.Title,
                        model.Film.PublishYear,
                        model.Film.Description,
                        Actors = selectedActors
                    });
        }

        #endregion
    }
}
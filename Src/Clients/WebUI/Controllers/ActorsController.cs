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
    public class ActorsController : Controller
    {
        private readonly IApiTools _apiTools;

        public ActorsController()
        {
            _apiTools = new ApiTools();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View((await _apiTools.FetchAsync<ActorsListReturnModel>("https://localhost:5001/api/actors/getall"))
                .Actors
                .TakeLast(10));
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(int id)
        {
            return View(await PrepareAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate([FromForm] CreateOrUpdateActorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await PrepareAsync(model);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: To local Consts.cs.
            await _apiTools.DeleteAsync($"https://localhost:5001/api/actors/delete/{id}");

            return Ok();
        }

        #region Helpers

        private async Task<CreateOrUpdateActorViewModel> PrepareAsync(int id)
        {
            ActorReturnModel actor;
            IEnumerable<int> selectedFilms = null;

            if (id == 0)
            {
                actor = new ActorReturnModel();
            }
            else
            {
                actor = await _apiTools.FetchAsync<ActorReturnModel>($"https://localhost:5001/api/actors/get/{id}");
                selectedFilms =
                    (await _apiTools.FetchAsync<FilmsListReturnModel>(
                        $"https://localhost:5001/api/films/getallbyactor/{id}")).Films.Select(x => x.FilmId);
            }

            return new CreateOrUpdateActorViewModel
            {
                Actor = actor,

                Films = (await _apiTools.FetchAsync<FilmsListReturnModel>("https://localhost:5001/api/films/getall"))
                    .Films
                    .TakeLast(10)
                    .Select(x => new SelectListItem
                    {
                        Value = x.FilmId.ToString(),
                        Text = $"{x.Title} ({x.PublishYear.Year})"
                    }),

                SelectedFilms = selectedFilms
            };
        }

        private async Task<IEnumerable<FilmReturnModel>> FetchSelectedFilmsAsync(CreateOrUpdateActorViewModel model)
        {
            var actors = new List<FilmReturnModel>();

            if (model.IsNopeFilms) return actors;
            if (model.SelectedFilms == null) return actors;

            foreach (var selectedFilmId in model.SelectedFilms)
                actors.Add(await _apiTools.FetchAsync<FilmReturnModel>(
                    $"https://localhost:5001/api/films/get/{selectedFilmId}"));

            return actors;
        }

        private async Task PrepareAsync(CreateOrUpdateActorViewModel model)
        {
            var selectedFilms = await FetchSelectedFilmsAsync(model);

            if (model.Actor.ActorId == 0)
                await _apiTools.PostAsync<ActorReturnModel>("https://localhost:5001/api/actors/create",
                    new {model.Actor.FirstName, model.Actor.LastName, model.Actor.Birthday, Films = selectedFilms});
            else
                await _apiTools.PostAsync<ActorReturnModel>("https://localhost:5001/api/actors/update",
                    new
                    {
                        model.Actor.ActorId,
                        model.Actor.FirstName,
                        model.Actor.LastName,
                        model.Actor.Birthday,
                        Films = selectedFilms
                    });
        }

        #endregion
    }
}
using System.Linq;
using System.Threading.Tasks;
using Exam.Clients.WebUI.Common.Helpers.ApiTools;
using Exam.Clients.WebUI.Common.Models.Returnable.Film;
using Microsoft.AspNetCore.Mvc;

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
            var model = await _apiTools.FetchAsync<FilmsListReturnModel>("https://localhost:5001/api/films/getall");

            return View(model.Films.TakeLast(10));
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrUpdate(int id)
        {
            // TODO: To local Consts.cs.
            var model = id == 0
                ? new FilmReturnModel()
                : await _apiTools.FetchAsync<FilmReturnModel>($"https://localhost:5001/api/films/get/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate([FromForm] FilmReturnModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // TODO: To local Consts.cs.
            var uri = model.FilmId == 0
                ? "https://localhost:5001/api/films/create"
                : "https://localhost:5001/api/films/update";

            await _apiTools.PostAsync<FilmReturnModel>(uri, model);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: To local Consts.cs.
            await _apiTools.DeleteAsync($"https://localhost:5001/api/films/delete/{id}");

            return Ok();
        }
    }
}
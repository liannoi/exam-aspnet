using Microsoft.AspNetCore.Mvc;

namespace Exam.Clients.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
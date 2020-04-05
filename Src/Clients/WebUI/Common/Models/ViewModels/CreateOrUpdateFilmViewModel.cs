using System.Collections.Generic;
using Exam.Clients.WebUI.Common.Models.Returnable.Film;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exam.Clients.WebUI.Common.Models.ViewModels
{
    public class CreateOrUpdateFilmViewModel
    {
        public FilmReturnModel Film { get; set; }

        public IEnumerable<int> SelectedActors { get; set; }
        public IEnumerable<SelectListItem> Actors { get; set; }
    }
}
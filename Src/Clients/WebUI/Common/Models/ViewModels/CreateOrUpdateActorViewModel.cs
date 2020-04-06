using System.Collections.Generic;
using Exam.Clients.WebUI.Common.Models.Returnable.Actor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Exam.Clients.WebUI.Common.Models.ViewModels
{
    public class CreateOrUpdateActorViewModel
    {
        public ActorReturnModel Actor { get; set; }

        public IEnumerable<int> SelectedFilms { get; set; }
        public IEnumerable<SelectListItem> Films { get; set; }
        public bool IsNopeFilms { get; set; }
    }
}
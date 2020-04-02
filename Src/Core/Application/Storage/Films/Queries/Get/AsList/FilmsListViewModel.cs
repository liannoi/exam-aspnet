using System.Collections.Generic;

namespace Exam.Application.Storage.Films.Queries.Get.AsList
{
    public class FilmsListViewModel
    {
        public IList<FilmLookupDto> Films { get; set; }
    }
}
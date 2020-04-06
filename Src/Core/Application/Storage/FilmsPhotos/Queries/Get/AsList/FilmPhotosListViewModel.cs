using System.Collections.Generic;

namespace Exam.Application.Storage.FilmsPhotos.Queries.Get.AsList
{
    public class FilmPhotosListViewModel
    {
        public IList<FilmPhotoLookupDto> FilmPhotos { get; set; }
    }
}
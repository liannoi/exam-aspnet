using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Genre
    {
        public Genre()
        {
            FilmsGenres = new HashSet<FilmsGenres>();
        }

        public int GenreId { get; set; }
        public string Title { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once CollectionNeverUpdated.Global
        public ICollection<FilmsGenres> FilmsGenres { get; private set; }
    }
}
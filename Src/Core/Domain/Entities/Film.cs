using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Film
    {
        public Film()
        {
            FilmsPhotos = new HashSet<FilmsPhoto>();
            Actors = new HashSet<Actor>();
            Genres = new HashSet<Genre>();
        }

        public int FilmId { get; set; }
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<FilmsPhoto> FilmsPhotos { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<Actor> Actors { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<Genre> Genres { get; private set; }
    }
}
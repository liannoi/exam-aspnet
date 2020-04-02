using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Film
    {
        public Film()
        {
            ActorFilms = new HashSet<ActorsFilms>();
            FilmGenres = new HashSet<FilmsGenres>();
            FilmsPhotos = new HashSet<FilmPhoto>();
        }

        public int FilmId { get; set; }
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<ActorsFilms> ActorFilms { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<FilmsGenres> FilmGenres { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<FilmPhoto> FilmsPhotos { get; private set; }
    }
}
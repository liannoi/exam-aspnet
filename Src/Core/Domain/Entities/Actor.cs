using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Actor
    {
        public Actor()
        {
            ActorsFilms = new HashSet<ActorsFilms>();
            ActorsPhotos = new HashSet<ActorPhoto>();
        }

        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once CollectionNeverUpdated.Global
        public ICollection<ActorsFilms> ActorsFilms { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once CollectionNeverUpdated.Global
        public ICollection<ActorPhoto> ActorsPhotos { get; private set; }
    }
}
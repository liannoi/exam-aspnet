using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Actor
    {
        public Actor()
        {
            ActorsPhotos = new HashSet<ActorsPhoto>();
            Films = new HashSet<Film>();
        }

        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<ActorsPhoto> ActorsPhotos { get; private set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICollection<Film> Films { get; private set; }
    }
}
using System;
using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.Actors
{
    public class ActorLookupDto : IMapFrom<Actor>
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Actor, ActorLookupDto>()
                .ForMember(d => d.ActorId, opt => opt.MapFrom(s => s.ActorId))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Birthday, opt => opt.MapFrom(s => s.Birthday));
        }
    }
}
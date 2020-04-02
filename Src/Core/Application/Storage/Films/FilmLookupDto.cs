using System;
using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.Films
{
    public class FilmLookupDto : IMapFrom<Film>
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Film, FilmLookupDto>()
                .ForMember(d => d.FilmId, opt => opt.MapFrom(s => s.FilmId))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.PublishYear, opt => opt.MapFrom(s => s.PublishYear))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}
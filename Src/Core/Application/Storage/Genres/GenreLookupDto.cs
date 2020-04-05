using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.Genres
{
    public class GenreLookupDto : IMapFrom<Genre>
    {
        public int GenreId { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreLookupDto>()
                .ForMember(d => d.GenreId, opt => opt.MapFrom(s => s.GenreId))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title));
        }
    }
}
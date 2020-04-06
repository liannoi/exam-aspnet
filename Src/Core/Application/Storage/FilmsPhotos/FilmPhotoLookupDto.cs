using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.FilmsPhotos
{
    public class FilmPhotoLookupDto : IMapFrom<FilmPhoto>
    {
        public int PhotoId { get; set; }
        public int FilmId { get; set; }
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FilmPhoto, FilmPhotoLookupDto>()
                .ForMember(d => d.PhotoId, opt => opt.MapFrom(s => s.PhotoId))
                .ForMember(d => d.FilmId, opt => opt.MapFrom(s => s.FilmId))
                .ForMember(d => d.Path, opt => opt.MapFrom(s => s.Path));
        }
    }
}
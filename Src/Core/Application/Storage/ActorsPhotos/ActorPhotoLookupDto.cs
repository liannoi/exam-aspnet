using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.ActorsPhotos
{
    public class ActorPhotoLookupDto : IMapFrom<ActorPhoto>
    {
        public int PhotoId { get; set; }
        public int ActorId { get; set; }
        public string Path { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ActorPhoto, ActorPhotoLookupDto>()
                .ForMember(e => e.PhotoId, opt => opt.MapFrom(s => s.PhotoId))
                .ForMember(e => e.ActorId, opt => opt.MapFrom(s => s.ActorId))
                .ForMember(e => e.Path, opt => opt.MapFrom(s => s.Path));
        }
    }
}
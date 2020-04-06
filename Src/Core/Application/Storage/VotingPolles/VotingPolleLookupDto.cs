using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.VotingPolles
{
    public class VotingPolleLookupDto : IMapFrom<VotingPolleRelation>
    {
        public int VotingPolleId { get; set; }
        public int VotingId { get; set; }
        public int PolleId { get; set; }
        public int VotingAnswerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VotingPolleRelation, VotingPolleLookupDto>()
                .ForMember(d => d.VotingPolleId, opt => opt.MapFrom(s => s.VotingPolleId))
                .ForMember(d => d.VotingId, opt => opt.MapFrom(s => s.VotingId))
                .ForMember(d => d.PolleId, opt => opt.MapFrom(s => s.PolleId))
                .ForMember(d => d.VotingAnswerId, opt => opt.MapFrom(s => s.VotingAnswerId));
        }
    }
}
using AutoMapper;
using Exam.Application.Common.Mappings;

namespace Exam.Application.Storage.Voting
{
    public class VotingLookupDto : IMapFrom<Domain.Entities.Voting>
    {
        public int VotingId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Voting, VotingLookupDto>()
                .ForMember(d => d.VotingId, opt => opt.MapFrom(s => s.VotingId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
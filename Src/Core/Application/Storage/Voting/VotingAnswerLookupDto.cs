using AutoMapper;
using Exam.Application.Common.Mappings;
using Exam.Domain.Entities;
using Newtonsoft.Json;

namespace Exam.Application.Storage.Voting
{
    public class VotingAnswerLookupDto : IMapFrom<VotingAnswer>
    {
        [JsonProperty("votingAnswerId")] public int VotingAnswerId { get; set; }

        [JsonProperty("votingId")] public int VotingId { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VotingAnswer, VotingAnswerLookupDto>()
                .ForMember(d => d.VotingAnswerId, opt => opt.MapFrom(s => s.VotingAnswerId))
                .ForMember(d => d.VotingId, opt => opt.MapFrom(s => s.VotingId))
                .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Text));
        }
    }
}
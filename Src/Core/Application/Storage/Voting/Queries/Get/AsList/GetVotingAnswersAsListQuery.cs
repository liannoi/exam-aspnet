using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Voting.Queries.Get.AsList
{
    public class GetVotingAnswersAsListQuery : IRequest<VotingAnswersListViewModel>
    {
        public int VotingId { get; set; }

        public class
            GetVotingAnswersAsListQueryHandler : IRequestHandler<GetVotingAnswersAsListQuery, VotingAnswersListViewModel
            >
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetVotingAnswersAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<VotingAnswersListViewModel> Handle(GetVotingAnswersAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new VotingAnswersListViewModel
                {
                    VotingAnswers = await _context.VotingAnswers
                        .Where(e => e.VotingId == request.VotingId)
                        .ProjectTo<VotingAnswerLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
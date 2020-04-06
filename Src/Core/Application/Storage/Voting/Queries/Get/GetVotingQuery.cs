using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Voting.Queries.Get
{
    public class GetVotingQuery : IRequest<VotingLookupDto>
    {
        public int VotingId { get; set; }

        public class GetVotingQueryHandler : IRequestHandler<GetVotingQuery, VotingLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetVotingQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<VotingLookupDto> Handle(GetVotingQuery request, CancellationToken cancellationToken)
            {
                return await _context.Voting
                    .Where(e => e.VotingId == request.VotingId)
                    .ProjectTo<VotingLookupDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
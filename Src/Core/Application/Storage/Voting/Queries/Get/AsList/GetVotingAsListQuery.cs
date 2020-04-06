using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Voting.Queries.Get.AsList
{
    public class GetVotingAsListQuery : IRequest<VotingListViewModel>
    {
        public class GetVotingAsListQueryHandler : IRequestHandler<GetVotingAsListQuery, VotingListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetVotingAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<VotingListViewModel> Handle(GetVotingAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new VotingListViewModel
                {
                    Voting = await _context.Voting
                        .ProjectTo<VotingLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
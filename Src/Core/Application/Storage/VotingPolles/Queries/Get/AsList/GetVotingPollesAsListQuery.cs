using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.VotingPolles.Queries.Get.AsList
{
    public class GetVotingPollesAsListQuery : IRequest<VotingPollesListViewModel>
    {
        public class
            GetVotingPollesAsListQueryHandler : IRequestHandler<GetVotingPollesAsListQuery, VotingPollesListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetVotingPollesAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<VotingPollesListViewModel> Handle(GetVotingPollesAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new VotingPollesListViewModel
                {
                    VotingPolles = await _context.VotingPolles
                        .ProjectTo<VotingPolleLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
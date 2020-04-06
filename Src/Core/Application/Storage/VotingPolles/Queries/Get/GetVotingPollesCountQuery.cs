using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using Exam.Application.Storage.VotingPolles.Queries.Get.AsList;
using MediatR;

namespace Exam.Application.Storage.VotingPolles.Queries.Get
{
    public class GetVotingPollesCountQuery : IRequest<VotingPollesListViewModel>
    {
        public int VotingId { get; set; }

        public class
            GetVotingPollesByVotingQueryHandler : IRequestHandler<GetVotingPollesCountQuery, VotingPollesListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetVotingPollesByVotingQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<VotingPollesListViewModel> Handle(GetVotingPollesCountQuery request,
                CancellationToken cancellationToken)
            {
                return new VotingPollesListViewModel
                {
                    VotingPolles = _context.VotingPolles
                        .Where(e => e.VotingId == request.VotingId)
                        .ProjectTo<VotingPolleLookupDto>(_mapper.ConfigurationProvider)
                        .ToList()
                };
            }
        }
    }
}
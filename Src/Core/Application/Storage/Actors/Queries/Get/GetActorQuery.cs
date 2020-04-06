using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Actors.Queries.Get
{
    public class GetActorQuery : IRequest<ActorLookupDto>
    {
        public int ActorId { get; set; }

        public class GetActorQueryHandler : IRequestHandler<GetActorQuery, ActorLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetActorQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorLookupDto> Handle(GetActorQuery request, CancellationToken cancellationToken)
            {
                return await _context.Actors
                    .Where(e => e.ActorId == request.ActorId)
                    .ProjectTo<ActorLookupDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
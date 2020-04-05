using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Actors.Queries.Get.AsList
{
    public class GetActorsAsListQuery : IRequest<ActorsListViewModel>
    {
        public class GetActorsAsListQueryHandler : IRequestHandler<GetActorsAsListQuery, ActorsListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetActorsAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorsListViewModel> Handle(GetActorsAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new ActorsListViewModel
                {
                    Actors = await _context.Actors
                        .ProjectTo<ActorLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
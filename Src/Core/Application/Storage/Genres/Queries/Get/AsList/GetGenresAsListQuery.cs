using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Genres.Queries.Get.AsList
{
    public class GetGenresAsListQuery : IRequest<GenresListViewModel>
    {
        public class GetGenresAsListQueryHandler : IRequestHandler<GetGenresAsListQuery, GenresListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetGenresAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GenresListViewModel> Handle(GetGenresAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new GenresListViewModel
                {
                    Genres = await _context.Genres
                        .ProjectTo<GenreLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
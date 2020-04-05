using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Genres.Queries.Get
{
    public class GetGenreQuery : IRequest<GenreLookupDto>
    {
        public int GenreId { get; set; }

        public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, GenreLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetGenreQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GenreLookupDto> Handle(GetGenreQuery request, CancellationToken cancellationToken)
            {
                return await _context.Genres
                    .Where(e => e.GenreId == request.GenreId)
                    .ProjectTo<GenreLookupDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Films.Queries.Get
{
    public class GetFilmQuery : IRequest<FilmLookupDto>
    {
        public int Id { get; set; }

        public class GetFilmQueryHandler : IRequestHandler<GetFilmQuery, FilmLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetFilmQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmLookupDto> Handle(GetFilmQuery request, CancellationToken cancellationToken)
            {
                return await _context.Films
                    .Where(e => e.FilmId == request.Id)
                    .ProjectTo<FilmLookupDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Films.Queries.Get.AsList.ByGenre
{
    public class GetFilmsByGenreAsListQuery : IRequest<FilmsListViewModel>
    {
        public int GenreId { get; set; }

        public class GetFilmsByGenreAsListQueryHandler : IRequestHandler<GetFilmsByGenreAsListQuery, FilmsListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetFilmsByGenreAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmsListViewModel> Handle(GetFilmsByGenreAsListQuery request,
                CancellationToken cancellationToken)
            {
                var selected = new List<FilmLookupDto>();

                await _context.FilmsGenres
                    .Where(e => e.GenreId == request.GenreId)
                    .ForEachAsync(async e =>
                    {
                        selected.Add(_mapper.Map<FilmLookupDto>(await _context.Films
                            .Where(x => x.FilmId == e.FilmId)
                            .FirstOrDefaultAsync(cancellationToken)));
                    }, cancellationToken);

                return new FilmsListViewModel
                    {Films = selected.GroupBy(x => x.FilmId).Select(x => x.FirstOrDefault()).ToList()};
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Genres.Queries.Get.AsList.ByFilm
{
    public class GetGenresByFilmAsListQuery : IRequest<GenresListViewModel>
    {
        public int FilmId { get; set; }

        public class
            GetGenresByFilmAsListQueryHandler : IRequestHandler<GetGenresByFilmAsListQuery, GenresListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetGenresByFilmAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GenresListViewModel> Handle(GetGenresByFilmAsListQuery request,
                CancellationToken cancellationToken)
            {
                var selected = new List<GenreLookupDto>();

                await _context.FilmsGenres
                    .Where(e => e.FilmId == request.FilmId)
                    .ForEachAsync(async e =>
                    {
                        selected.Add(_mapper.Map<GenreLookupDto>(await _context.Films
                            .Where(x => x.FilmId == e.FilmId)
                            .FirstOrDefaultAsync(cancellationToken)));
                    }, cancellationToken);

                return new GenresListViewModel
                    {Genres = selected.GroupBy(x => x.GenreId).Select(x => x.FirstOrDefault()).ToList()};
            }
        }
    }
}
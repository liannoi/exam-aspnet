using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Films.Queries.Get.AsList.ByActor
{
    public class GetFilmsByActorAsListQuery : IRequest<FilmsListViewModel>
    {
        public int ActorId { get; set; }

        public class GetFilmsByActorAsListQueryHandler : IRequestHandler<GetFilmsByActorAsListQuery, FilmsListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetFilmsByActorAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmsListViewModel> Handle(GetFilmsByActorAsListQuery request,
                CancellationToken cancellationToken)
            {
                var selected = new List<FilmLookupDto>();

                await _context.ActorsFilms
                    .Where(e => e.ActorId == request.ActorId)
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
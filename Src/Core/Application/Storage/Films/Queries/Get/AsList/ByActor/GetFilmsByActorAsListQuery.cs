using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;

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

                var collection = _context.ActorsFilms.Where(e => e.ActorId == request.ActorId);
                foreach (var item in collection)
                    selected.Add(_context.Films
                        .ProjectTo<FilmLookupDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefault(e => e.FilmId == item.FilmId));

                return new FilmsListViewModel
                    {Films = selected.GroupBy(x => x.FilmId).Select(x => x.FirstOrDefault()).ToList()};
            }
        }
    }
}
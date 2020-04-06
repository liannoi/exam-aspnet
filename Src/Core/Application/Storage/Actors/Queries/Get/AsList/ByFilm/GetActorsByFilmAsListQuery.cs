using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;

namespace Exam.Application.Storage.Actors.Queries.Get.AsList.ByFilm
{
    public class GetActorsByFilmAsListQuery : IRequest<ActorsListViewModel>
    {
        public int FilmId { get; set; }

        public class
            GetActorsByFilmAsListQueryHandler : IRequestHandler<GetActorsByFilmAsListQuery, ActorsListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetActorsByFilmAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorsListViewModel> Handle(GetActorsByFilmAsListQuery request,
                CancellationToken cancellationToken)
            {
                var selected = new List<ActorLookupDto>();

                var collection = _context.ActorsFilms.Where(e => e.FilmId == request.FilmId);
                foreach (var item in collection)
                    selected.Add(_context.Actors
                        .ProjectTo<ActorLookupDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefault(e => e.ActorId == item.ActorId));

                return new ActorsListViewModel
                    {Actors = selected.GroupBy(x => x.ActorId).Select(x => x.FirstOrDefault()).ToList()};
            }
        }
    }
}
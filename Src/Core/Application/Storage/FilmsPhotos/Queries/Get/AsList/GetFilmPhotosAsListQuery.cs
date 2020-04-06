using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.FilmsPhotos.Queries.Get.AsList
{
    public class GetFilmPhotosAsListQuery : IRequest<FilmPhotosListViewModel>
    {
        public int FilmId { get; set; }

        public class
            GetFilmPhotosAsListQueryHandler : IRequestHandler<GetFilmPhotosAsListQuery, FilmPhotosListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetFilmPhotosAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmPhotosListViewModel> Handle(GetFilmPhotosAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new FilmPhotosListViewModel
                {
                    FilmPhotos = await _context.FilmPhotos
                        .Where(e => e.FilmId == request.FilmId)
                        .ProjectTo<FilmPhotoLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
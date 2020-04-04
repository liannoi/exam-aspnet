using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Films.Queries.Get.Base.AsList
{
    public class GetFilmsAsListQuery : IRequest<FilmsListViewModel>
    {
        // ReSharper disable once UnusedType.Global
        public class GetFilmsAsListQueryHandler : IRequestHandler<GetFilmsAsListQuery, FilmsListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetFilmsAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmsListViewModel> Handle(GetFilmsAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new FilmsListViewModel
                {
                    Films = await _context.Films
                        .ProjectTo<FilmLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
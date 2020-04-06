using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.ActorsPhotos.Queries.Get.AsList
{
    public class GetActorPhotosAsListQuery : IRequest<ActorPhotosListViewModel>
    {
        public int ActorId { get; set; }

        public class
            GetActorPhotosAsListQueryHandler : IRequestHandler<GetActorPhotosAsListQuery, ActorPhotosListViewModel>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public GetActorPhotosAsListQueryHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorPhotosListViewModel> Handle(GetActorPhotosAsListQuery request,
                CancellationToken cancellationToken)
            {
                return new ActorPhotosListViewModel
                {
                    ActorPhotos = await _context.ActorPhotos
                        .Where(e => e.ActorId == request.ActorId)
                        .ProjectTo<ActorPhotoLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Actors.Commands.Delete
{
    public class DeleteActorCommand : IRequest<ActorLookupDto>
    {
        public int ActorId { get; set; }

        public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand, ActorLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public DeleteActorCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorLookupDto> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
            {
                var fined = await _context.Actors
                    .Where(e => e.ActorId == request.ActorId)
                    .FirstOrDefaultAsync(cancellationToken);

                #region Work with dependencies

                await ClearFilmsInActorAsync(request, cancellationToken);

                #endregion

                _context.Actors.Remove(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ActorLookupDto>(fined);
            }

            private async Task ClearFilmsInActorAsync(DeleteActorCommand request, CancellationToken cancellationToken)
            {
                var filmsInActor = _context.ActorsFilms.Where(e => e.ActorId == request.ActorId);
                foreach (var item in filmsInActor) _context.ActorsFilms.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
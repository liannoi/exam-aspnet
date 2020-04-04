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

                // TODO: Improve this logic, realizing the fact that the film has the connecting factors: photos, actors and genres. When deleting a film or connecting factors, (you must also request a “deletion mode” or automatic)? - in which we will remove the binders immediately, or manual - in which the removal will be manually a new request.

                _context.Actors.Remove(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ActorLookupDto>(fined);
            }
        }
    }
}
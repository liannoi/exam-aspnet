using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Actors.Commands.Update
{
    public class UpdateActorCommand : IRequest<ActorLookupDto>
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand, ActorLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public UpdateActorCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorLookupDto> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
            {
                var fined = await _context.Actors
                    .Where(e => e.ActorId == request.ActorId)
                    .FirstOrDefaultAsync(cancellationToken);

                fined.FirstName = request.FirstName;
                fined.LastName = request.LastName;
                fined.Birthday = request.Birthday;
                _context.Actors.Update(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ActorLookupDto>(fined);
            }
        }
    }
}
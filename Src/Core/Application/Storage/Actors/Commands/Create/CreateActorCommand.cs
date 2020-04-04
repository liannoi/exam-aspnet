using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.Actors.Commands.Create
{
    public class CreateActorCommand : IRequest<ActorLookupDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, ActorLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public CreateActorCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ActorLookupDto> Handle(CreateActorCommand request, CancellationToken cancellationToken)
            {
                var result = await _context.Actors.AddAsync(new Actor
                {
                    Birthday = request.Birthday,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                }, cancellationToken);

                // TODO: Improve this logic, realizing the fact that the film has the connecting factors: photos, actors and genres. When deleting a film or connecting factors, (you must also request a “deletion mode” or automatic)? - in which we will remove the binders immediately, or manual - in which the removal will be manually a new request.

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ActorLookupDto>(result.Entity);
            }
        }
    }
}
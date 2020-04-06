using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using Exam.Application.Storage.Films;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.Actors.Commands.Create
{
    public class CreateActorCommand : IRequest<ActorLookupDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public IEnumerable<FilmLookupDto> Films { get; set; }

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

                await _context.SaveChangesAsync(cancellationToken);

                #region Work with dependencies

                await AddFilmsInActorAsync(result.Entity, request, cancellationToken);

                #endregion

                return _mapper.Map<ActorLookupDto>(result.Entity);
            }

            private async Task AddFilmsInActorAsync(Actor actor, CreateActorCommand request,
                CancellationToken cancellationToken)
            {
                if (!request.Films.Any()) return;

                foreach (var film in request.Films)
                    await _context.ActorsFilms
                        .AddAsync(new ActorsFilms {FilmId = film.FilmId, ActorId = actor.ActorId}, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
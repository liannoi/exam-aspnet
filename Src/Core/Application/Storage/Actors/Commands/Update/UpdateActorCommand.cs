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
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Actors.Commands.Update
{
    public class UpdateActorCommand : IRequest<ActorLookupDto>
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public IEnumerable<FilmLookupDto> Films { get; set; }

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

                #region Work with dependencies

                await UpdateFilmsInActorAsync(fined, request, cancellationToken);

                #endregion

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ActorLookupDto>(fined);
            }

            private async Task UpdateFilmsInActorAsync(Actor actor, UpdateActorCommand request,
                CancellationToken cancellationToken)
            {
                _context.ActorsFilms
                    .Where(e => e.ActorId == request.ActorId)
                    .ToList()
                    .ForEach(e => { _context.ActorsFilms.Remove(e); });

                if (!request.Films.Any()) return;

                foreach (var film in request.Films)
                    await _context.ActorsFilms
                        .AddAsync(new ActorsFilms {FilmId = film.FilmId, ActorId = actor.ActorId}, cancellationToken);
            }
        }
    }
}
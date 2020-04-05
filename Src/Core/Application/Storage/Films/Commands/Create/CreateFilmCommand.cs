using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using Exam.Application.Storage.Actors;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.Films.Commands.Create
{
    public class CreateFilmCommand : IRequest<FilmLookupDto>
    {
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }

        public IEnumerable<ActorLookupDto> Actors { get; set; }

        public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, FilmLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public CreateFilmCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmLookupDto> Handle(CreateFilmCommand request, CancellationToken cancellationToken)
            {
                var result = await _context.Films.AddAsync(new Film
                {
                    Description = request.Description,
                    PublishYear = request.PublishYear,
                    Title = request.Title
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                await ClearActorsInFilmAsync(result.Entity, request, cancellationToken);

                return _mapper.Map<FilmLookupDto>(result.Entity);
            }

            private async Task ClearActorsInFilmAsync(Film film, CreateFilmCommand request,
                CancellationToken cancellationToken)
            {
                if (!request.Actors.Any()) return;

                foreach (var actor in request.Actors)
                    await _context.ActorsFilms
                        .AddAsync(new ActorsFilms {FilmId = film.FilmId, ActorId = actor.ActorId}, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
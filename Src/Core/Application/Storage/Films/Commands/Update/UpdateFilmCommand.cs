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
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Films.Commands.Update
{
    public class UpdateFilmCommand : IRequest<FilmLookupDto>
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }

        public IEnumerable<ActorLookupDto> Actors { get; set; }

        public class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand, FilmLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public UpdateFilmCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmLookupDto> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
            {
                var fined = await _context.Films
                    .Where(e => e.FilmId == request.FilmId)
                    .FirstOrDefaultAsync(cancellationToken);

                fined.Title = request.Title;
                fined.PublishYear = request.PublishYear;
                fined.Description = request.Description;
                _context.Films.Update(fined);

                #region Work with dependencies

                await UpdateActorsInFilmAsync(fined, request, cancellationToken);

                #endregion

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<FilmLookupDto>(fined);
            }

            private async Task UpdateActorsInFilmAsync(Film film, UpdateFilmCommand request,
                CancellationToken cancellationToken)
            {
                _context.ActorsFilms
                    .Where(e => e.FilmId == request.FilmId)
                    .ToList()
                    .ForEach(e => { _context.ActorsFilms.Remove(e); });

                if (!request.Actors.Any()) return;

                foreach (var actor in request.Actors)
                    await _context.ActorsFilms
                        .AddAsync(new ActorsFilms {FilmId = film.FilmId, ActorId = actor.ActorId}, cancellationToken);
            }
        }
    }
}
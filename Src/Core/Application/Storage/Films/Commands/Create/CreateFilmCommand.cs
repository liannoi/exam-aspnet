using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.Films.Commands.Create
{
    public class CreateFilmCommand : IRequest<FilmLookupDto>
    {
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }
        public string Description { get; set; }

        // TODO: Add only after the start of the client description on MVC. There will be three collections of connecting factors, and when we transfer an empty collection (on the client we display through "(Nope)" and a static photo for the absence of a photo) - that means we skip adding in the Handle method (add checks for all three collections there).

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

                #region Temporary

                //_context.FilmsGenres.Add(new FilmsGenres(){})
                //_context.ActorsFilms.Add(new ActorsFilms(){})
                //_context.FilmPhotos.Add(new FilmPhoto(){})

                #endregion

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<FilmLookupDto>(result.Entity);
            }
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
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

        // TODO: Add only after the start of the client description on MVC. There will be three collections of connecting factors, and when we transfer an empty collection (on the client we display through "(Nope)" and a static photo for the absence of a photo) - that means we skip adding in the Handle method (add checks for all three collections there).

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
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<FilmLookupDto>(fined);
            }
        }
    }
}
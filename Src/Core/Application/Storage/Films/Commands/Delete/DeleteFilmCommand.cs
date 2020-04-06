using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Films.Commands.Delete
{
    public class DeleteFilmCommand : IRequest<FilmLookupDto>
    {
        public int FilmId { get; set; }

        public class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, FilmLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public DeleteFilmCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmLookupDto> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
            {
                var fined = await _context.Films
                    .Where(e => e.FilmId == request.FilmId)
                    .FirstOrDefaultAsync(cancellationToken);

                #region Work with dependencies

                await ClearActorsInFilmAsync(request, cancellationToken);

                #endregion

                _context.Films.Remove(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<FilmLookupDto>(fined);
            }

            private async Task ClearActorsInFilmAsync(DeleteFilmCommand request, CancellationToken cancellationToken)
            {
                var actorsInFilms = _context.ActorsFilms.Where(e => e.FilmId == request.FilmId);
                foreach (var item in actorsInFilms) _context.ActorsFilms.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
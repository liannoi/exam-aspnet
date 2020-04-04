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

                // TODO: Improve this logic, realizing the fact that the film has the connecting factors: photos, actors and genres. When deleting a film or connecting factors, (you must also request a “deletion mode” or automatic)? - in which we will remove the binders immediately, or manual - in which the removal will be manually a new request.

                _context.Films.Remove(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<FilmLookupDto>(fined);
            }
        }
    }
}
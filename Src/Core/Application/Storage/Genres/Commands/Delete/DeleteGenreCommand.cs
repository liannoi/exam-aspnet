using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Genres.Commands.Delete
{
    public class DeleteGenreCommand : IRequest<GenreLookupDto>
    {
        public int GenreId { get; set; }

        public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, GenreLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public DeleteGenreCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GenreLookupDto> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
            {
                var fined = await _context.Genres
                    .Where(e => e.GenreId == request.GenreId)
                    .FirstOrDefaultAsync(cancellationToken);

                _context.Genres.Remove(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<GenreLookupDto>(fined);
            }
        }
    }
}
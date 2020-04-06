using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Storage.Genres.Commands.Update
{
    public class UpdateGenreCommand : IRequest<GenreLookupDto>
    {
        public int GenreId { get; set; }
        public string Title { get; set; }

        public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public UpdateGenreCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GenreLookupDto> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
            {
                var fined = await _context.Genres
                    .Where(e => e.GenreId == request.GenreId)
                    .FirstOrDefaultAsync(cancellationToken);

                fined.Title = request.Title;
                _context.Genres.Update(fined);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<GenreLookupDto>(fined);
            }
        }
    }
}
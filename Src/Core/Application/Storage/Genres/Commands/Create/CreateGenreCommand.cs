using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.Genres.Commands.Create
{
    public class CreateGenreCommand : IRequest<GenreLookupDto>
    {
        public string Title { get; set; }

        public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, GenreLookupDto>
        {
            private readonly IFilmsDbContext _context;
            private readonly IMapper _mapper;

            public CreateGenreCommandHandler(IFilmsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GenreLookupDto> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
            {
                var result = await _context.Genres.AddAsync(new Genre {Title = request.Title}, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<GenreLookupDto>(result.Entity);
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using MediatR;

namespace Exam.Application.Storage.Seeding
{
    public class SeedingCommand : IRequest
    {
        // ReSharper disable once UnusedType.Global
        public class SeedingCommandHandler : IRequestHandler<SeedingCommand>
        {
            private readonly IFilmsDbContext _context;
            private readonly IJsonMocksReader<Actor> _mockActors;
            private readonly IJsonMocksReader<Film> _mockFilms;

            public SeedingCommandHandler(IFilmsDbContext context, IJsonMocksReader<Film> mockFilms,
                IJsonMocksReader<Actor> mockActors)
            {
                _context = context;
                _mockFilms = mockFilms;
                _mockActors = mockActors;
            }

            public async Task<Unit> Handle(SeedingCommand request, CancellationToken cancellationToken)
            {
                await new JsonMocksSeeder(_context, _mockFilms, _mockActors).SeedAllAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
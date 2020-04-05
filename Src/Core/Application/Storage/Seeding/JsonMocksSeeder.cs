using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;

namespace Exam.Application.Storage.Seeding
{
    public class JsonMocksSeeder
    {
        private readonly IFilmsDbContext _context;
        private readonly IJsonMocksReader<Actor> _mockActors;
        private readonly IJsonMocksReader<Film> _mockFilms;

        public JsonMocksSeeder(IFilmsDbContext context, IJsonMocksReader<Film> mockFilms,
            IJsonMocksReader<Actor> mockActors)
        {
            _context = context;
            _mockFilms = mockFilms;
            _mockActors = mockActors;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!_context.Films.Any())
                await _context.Films.AddRangeAsync(await _mockFilms.ReadAsync(Consts.FilmsMockPath, cancellationToken),
                    cancellationToken);

            if (!_context.Actors.Any())
                await _context.Actors.AddRangeAsync(
                    await _mockActors.ReadAsync(Consts.ActorsMockPath, cancellationToken), cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
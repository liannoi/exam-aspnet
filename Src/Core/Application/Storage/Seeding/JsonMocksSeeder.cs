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
            var films = (await _mockFilms.ReadAsync(Consts.FilmsMockPath, cancellationToken)).ToList();

            if (!_context.Films.Any())
                await _context.Films.AddRangeAsync(films, cancellationToken);

            if (!_context.Actors.Any())
                await _context.Actors.AddRangeAsync(
                    await _mockActors.ReadAsync(Consts.ActorsMockPath, cancellationToken), cancellationToken);

            if (!_context.Voting.Any())
                await _context.Voting.AddAsync(
                    new Domain.Entities.Voting {Name = "The best movie in your opinion..."}, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            if (!_context.VotingPolle.Any())
            {
                await _context.VotingPolle.AddAsync(new VotingPolle {VotingPolleId = 1}, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            if (!_context.VotingAnswers.Any())
            {
                var collection = films.TakeLast(5);
                foreach (var film in collection)
                    await _context.VotingAnswers
                        .AddAsync(new VotingAnswer {VotingId = 1, Text = film.Title}, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
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
        private readonly IJsonMocksReader<Film> _mockFilms;

        public JsonMocksSeeder(IFilmsDbContext context, IJsonMocksReader<Film> mockFilms)
        {
            _context = context;
            _mockFilms = mockFilms;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Films.Any()) return;

            await SeedMocksAsync(Consts.PathMockFilms, cancellationToken);
        }

        private async Task SeedMocksAsync(string filePath, CancellationToken cancellationToken)
        {
            await _context.Films.AddRangeAsync(
                await _mockFilms.ReadAsync(filePath, cancellationToken), cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exam.Domain.Entities;

namespace Exam.Infrastructure.MockReaders
{
    public class JsonFilmsMockReader : JsonMockReader<Film>
    {
        public override async Task<IEnumerable<Film>> ReadAsync(string filePath, CancellationToken cancellationToken)
        {
            return await await Task.Factory.StartNew(
                async () => (await DeserializeAsync(filePath, cancellationToken))
                    .GroupBy(x => x.Title)
                    .Select(x => x.FirstOrDefault()), cancellationToken);
        }
    }
}
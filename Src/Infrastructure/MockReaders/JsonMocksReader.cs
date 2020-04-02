using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Exam.Application.Common.Interfaces;
using Newtonsoft.Json;

namespace Exam.Infrastructure.MockReaders
{
    public class JsonMocksReader<TEntity> : IJsonMocksReader<TEntity> where TEntity : class, new()
    {
        public async Task<IEnumerable<TEntity>> ReadAsync(string filePath, CancellationToken cancellationToken)
        {
            return await await Task.Factory.StartNew(
                async () => JsonConvert.DeserializeObject<IEnumerable<TEntity>>(
                    await File.ReadAllTextAsync(filePath, cancellationToken)), cancellationToken);
        }
    }
}
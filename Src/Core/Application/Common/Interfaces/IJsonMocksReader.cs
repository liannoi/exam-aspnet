using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Exam.Application.Common.Interfaces
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IJsonMocksReader<TEntity> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> ReadAsync(string filePath, CancellationToken cancellationToken);
    }
}
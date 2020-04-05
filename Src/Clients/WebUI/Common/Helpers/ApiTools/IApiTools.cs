using System.Net.Http;
using System.Threading.Tasks;

namespace Exam.Clients.WebUI.Common.Helpers.ApiTools
{
    public interface IApiTools
    {
        Task<TEntity> FetchAsync<TEntity>(string uri) where TEntity : class, new();
        Task<TEntity> PostAsync<TEntity>(string uri, ByteArrayContent content) where TEntity : class, new();
        Task<TEntity> PostAsync<TEntity>(string uri, object value) where TEntity : class, new();
        Task DeleteAsync(string uri);
    }
}
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using Exam.Infrastructure.MockReaders;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection self)
        {
            self.AddTransient<IJsonMocksReader<Film>, JsonMocksReader<Film>>();

            return self;
        }
    }
}
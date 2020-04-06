using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using Exam.Infrastructure.MockReaders;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Infrastructure
{
    public static class DependencyInjection
    {
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static IServiceCollection AddInfrastructure(this IServiceCollection self)
        {
            self.AddTransient<IJsonMocksReader<Film>, JsonFilmsMockReader>();
            self.AddTransient<IJsonMocksReader<Actor>, JsonActorsMockReader>();

            return self;
        }
    }
}
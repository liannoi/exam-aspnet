using Exam.Application.Common.Interfaces;
using Exam.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection self, IConfiguration configuration)
        {
            self.AddDbContext<FilmsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(Consts.DatabaseNameInConnectionString)));

            self.AddScoped<IFilmsDbContext>(provider => provider.GetService<FilmsDbContext>());

            return self;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Exam.Application.Storage.Seeding;
using Exam.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Exam.Clients.WebApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    services.GetRequiredService<FilmsDbContext>().Database.Migrate();
                    await services.GetRequiredService<IMediator>().Send(new SeedingCommand(), CancellationToken.None);
                }
                catch (Exception ex)
                {
                    scope.ServiceProvider
                        .GetRequiredService<ILogger<Program>>()
                        .LogError(ex, "An error occurred while migrating or initializing the database.");
                }
            }

            host.Run();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                            true, true)
                        .AddJsonFile("appsettings.Local.json", true, true);

                    config.AddEnvironmentVariables();
                }).UseStartup<Startup>();
        }
    }
}
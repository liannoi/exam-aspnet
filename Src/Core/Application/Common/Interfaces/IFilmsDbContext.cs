using System.Threading;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exam.Application.Common.Interfaces
{
    public interface IFilmsDbContext
    {
        DbSet<Actor> Actors { get; set; }
        DbSet<ActorsPhoto> ActorsPhotos { get; set; }
        DbSet<Film> Films { get; set; }
        DbSet<FilmsPhoto> FilmsPhotos { get; set; }
        DbSet<Genre> Genres { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
using Exam.Application.Common.Interfaces;
using Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exam.Persistence.Context
{
    public class FilmsDbContext : DbContext, IFilmsDbContext
    {
        public FilmsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorsFilms> ActorsFilms { get; set; }
        public DbSet<ActorPhoto> ActorPhotos { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmsGenres> FilmsGenres { get; set; }
        public DbSet<FilmPhoto> FilmPhotos { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmsDbContext).Assembly);
        }
    }
}
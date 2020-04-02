using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Actors",
                table => new
                {
                    ActorId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 54),
                    LastName = table.Column<string>(maxLength: 54),
                    Birthday = table.Column<DateTime>("date")
                },
                constraints: table => { table.PrimaryKey("PK_Actors", x => x.ActorId); });

            migrationBuilder.CreateTable(
                "Films",
                table => new
                {
                    FilmId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 128),
                    PublishYear = table.Column<DateTime>("date"),
                    Description = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Films", x => x.FilmId); });

            migrationBuilder.CreateTable(
                "Genres",
                table => new
                {
                    GenreId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Genres", x => x.GenreId); });

            migrationBuilder.CreateTable(
                "ActorPhotos",
                table => new
                {
                    PhotoId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<int>(),
                    Path = table.Column<string>(maxLength: 512)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorPhotos", x => x.PhotoId);
                    table.ForeignKey(
                        "FK_ActorsPhotos_ActorId",
                        x => x.ActorId,
                        "Actors",
                        "ActorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "ActorsFilms",
                table => new
                {
                    ActorId = table.Column<int>(),
                    FilmId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorsFilms", x => new {x.ActorId, x.FilmId});
                    table.ForeignKey(
                        "FK_ActorsFilms_ActorId",
                        x => x.ActorId,
                        "Actors",
                        "ActorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_ActorsFilms_FilmId",
                        x => x.FilmId,
                        "Films",
                        "FilmId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FilmPhotos",
                table => new
                {
                    PhotoId = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(),
                    Path = table.Column<string>(maxLength: 512)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmPhotos", x => x.PhotoId);
                    table.ForeignKey(
                        "FK_FilmsPhotos_FilmId",
                        x => x.FilmId,
                        "Films",
                        "FilmId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "FilmsGenres",
                table => new
                {
                    FilmId = table.Column<int>(),
                    GenreId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsGenres", x => new {x.FilmId, x.GenreId});
                    table.ForeignKey(
                        "FK_FilmsGenres_FilmId",
                        x => x.FilmId,
                        "Films",
                        "FilmId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_FilmsGenres_GenreId",
                        x => x.GenreId,
                        "Genres",
                        "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_ActorPhotos_ActorId",
                "ActorPhotos",
                "ActorId");

            migrationBuilder.CreateIndex(
                "UNQ_ActorsPhotos_Path",
                "ActorPhotos",
                "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_ActorsFilms_FilmId",
                "ActorsFilms",
                "FilmId");

            migrationBuilder.CreateIndex(
                "IX_FilmPhotos_FilmId",
                "FilmPhotos",
                "FilmId");

            migrationBuilder.CreateIndex(
                "UNQ_FilmsPhotos_Path",
                "FilmPhotos",
                "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                "UNQ_Films_Title",
                "Films",
                "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_FilmsGenres_GenreId",
                "FilmsGenres",
                "GenreId");

            migrationBuilder.CreateIndex(
                "UNQ_Genres_Title",
                "Genres",
                "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ActorPhotos");

            migrationBuilder.DropTable(
                "ActorsFilms");

            migrationBuilder.DropTable(
                "FilmPhotos");

            migrationBuilder.DropTable(
                "FilmsGenres");

            migrationBuilder.DropTable(
                "Actors");

            migrationBuilder.DropTable(
                "Films");

            migrationBuilder.DropTable(
                "Genres");
        }
    }
}
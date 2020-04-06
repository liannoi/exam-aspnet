using FluentValidation;

namespace Exam.Application.Storage.Genres.Queries.Get.AsList.ByFilm
{
    public class GetGenresByFilmAsListQueryValidator : AbstractValidator<GetGenresByFilmAsListQuery>
    {
        public GetGenresByFilmAsListQueryValidator()
        {
            RuleFor(e => e.FilmId)
                .NotEmpty();
        }
    }
}
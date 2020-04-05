using FluentValidation;

namespace Exam.Application.Storage.Films.Queries.Get.AsList.ByGenre
{
    public class GetFilmsByGenreAsListQueryValidator : AbstractValidator<GetFilmsByGenreAsListQuery>
    {
        public GetFilmsByGenreAsListQueryValidator()
        {
            RuleFor(e => e.GenreId)
                .NotEmpty();
        }
    }
}
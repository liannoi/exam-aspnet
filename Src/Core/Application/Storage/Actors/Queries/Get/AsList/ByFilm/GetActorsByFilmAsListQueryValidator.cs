using FluentValidation;

namespace Exam.Application.Storage.Actors.Queries.Get.AsList.ByFilm
{
    public class GetActorsByFilmAsListQueryValidator : AbstractValidator<GetActorsByFilmAsListQuery>
    {
        public GetActorsByFilmAsListQueryValidator()
        {
            RuleFor(e => e.FilmId)
                .NotEmpty();
        }
    }
}
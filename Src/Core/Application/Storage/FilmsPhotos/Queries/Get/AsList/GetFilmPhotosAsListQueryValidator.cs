using FluentValidation;

namespace Exam.Application.Storage.FilmsPhotos.Queries.Get.AsList
{
    public class GetFilmPhotosAsListQueryValidator : AbstractValidator<GetFilmPhotosAsListQuery>
    {
        public GetFilmPhotosAsListQueryValidator()
        {
            RuleFor(e => e.FilmId)
                .NotEmpty();
        }
    }
}
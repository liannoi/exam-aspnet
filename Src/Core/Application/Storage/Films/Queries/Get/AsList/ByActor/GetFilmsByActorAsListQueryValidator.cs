using FluentValidation;

namespace Exam.Application.Storage.Films.Queries.Get.AsList.ByActor
{
    public class GetFilmsByActorAsListQueryValidator : AbstractValidator<GetFilmsByActorAsListQuery>
    {
        public GetFilmsByActorAsListQueryValidator()
        {
            RuleFor(e => e.ActorId)
                .NotEmpty();
        }
    }
}
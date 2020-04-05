using FluentValidation;

namespace Exam.Application.Storage.ActorsPhotos.Queries.Get.AsList
{
    public class GetActorPhotosAsListQueryValidator : AbstractValidator<GetActorPhotosAsListQuery>
    {
        public GetActorPhotosAsListQueryValidator()
        {
            RuleFor(e => e.ActorId)
                .NotEmpty();
        }
    }
}
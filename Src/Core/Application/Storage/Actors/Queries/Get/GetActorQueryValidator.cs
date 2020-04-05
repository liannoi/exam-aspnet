using FluentValidation;

namespace Exam.Application.Storage.Actors.Queries.Get
{
    public class GetActorQueryValidator : AbstractValidator<GetActorQuery>
    {
        public GetActorQueryValidator()
        {
            RuleFor(e => e.ActorId)
                .NotEmpty();
        }
    }
}
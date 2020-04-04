using FluentValidation;

namespace Exam.Application.Storage.Actors.Commands.Update
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(e => e.ActorId)
                .NotEmpty();

            RuleFor(e => e.Birthday)
                .NotEmpty();

            RuleFor(e => e.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(54);

            RuleFor(e => e.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(54);
        }
    }
}
using FluentValidation;

namespace Exam.Application.Storage.Actors.Commands.Create
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
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
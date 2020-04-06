using FluentValidation;

namespace Exam.Application.Storage.Films.Commands.Create
{
    public class CreateFilmCommandValidator : AbstractValidator<CreateFilmCommand>
    {
        public CreateFilmCommandValidator()
        {
            RuleFor(e => e.Description)
                .MaximumLength(4000);

            RuleFor(e => e.PublishYear)
                .NotEmpty();

            RuleFor(e => e.Title)
                .NotNull().WithMessage("Movie title cannot be empty.")
                .NotEmpty().WithMessage("Movie title cannot be empty.")
                .MaximumLength(128);
        }
    }
}
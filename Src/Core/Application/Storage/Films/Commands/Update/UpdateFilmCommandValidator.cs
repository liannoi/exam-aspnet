using FluentValidation;

namespace Exam.Application.Storage.Films.Commands.Update
{
    public class UpdateFilmCommandValidator : AbstractValidator<UpdateFilmCommand>
    {
        public UpdateFilmCommandValidator()
        {
            RuleFor(e => e.FilmId)
                .NotEmpty().WithMessage("The unique identifier of the film cannot be empty.");

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
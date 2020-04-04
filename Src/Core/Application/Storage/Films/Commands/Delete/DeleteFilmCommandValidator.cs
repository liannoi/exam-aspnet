using FluentValidation;

namespace Exam.Application.Storage.Films.Commands.Delete
{
    public class DeleteFilmCommandValidator : AbstractValidator<DeleteFilmCommand>
    {
        public DeleteFilmCommandValidator()
        {
            RuleFor(e => e.FilmId)
                .NotEmpty().WithMessage("The unique identifier of the film cannot be empty.");
        }
    }
}
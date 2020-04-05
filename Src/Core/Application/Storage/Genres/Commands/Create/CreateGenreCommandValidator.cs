using FluentValidation;

namespace Exam.Application.Storage.Genres.Commands.Create
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(e => e.Title)
                .NotNull()
                .NotEmpty();
        }
    }
}
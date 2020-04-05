using FluentValidation;

namespace Exam.Application.Storage.Genres.Commands.Delete
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(e => e.GenreId)
                .NotEmpty();
        }
    }
}
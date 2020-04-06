using FluentValidation;

namespace Exam.Application.Storage.Genres.Commands.Update
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(e => e.GenreId)
                .NotEmpty();

            RuleFor(e => e.Title)
                .NotNull()
                .NotEmpty();
        }
    }
}
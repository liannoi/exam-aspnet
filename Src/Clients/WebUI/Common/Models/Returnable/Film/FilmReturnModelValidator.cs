using FluentValidation;

namespace Exam.Clients.WebUI.Common.Models.Returnable.Film
{
    public class FilmReturnModelValidator : AbstractValidator<FilmReturnModel>
    {
        public FilmReturnModelValidator()
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
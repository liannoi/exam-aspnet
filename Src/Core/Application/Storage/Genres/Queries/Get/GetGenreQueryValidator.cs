using FluentValidation;

namespace Exam.Application.Storage.Genres.Queries.Get
{
    public class GetGenreQueryValidator : AbstractValidator<GetGenreQuery>
    {
        public GetGenreQueryValidator()
        {
            RuleFor(e => e.GenreId)
                .NotEmpty();
        }
    }
}
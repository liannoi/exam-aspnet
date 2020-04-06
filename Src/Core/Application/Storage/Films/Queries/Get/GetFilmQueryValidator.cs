using FluentValidation;

namespace Exam.Application.Storage.Films.Queries.Get
{
    public class GetFilmQueryValidator : AbstractValidator<GetFilmQuery>
    {
        public GetFilmQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty();
        }
    }
}
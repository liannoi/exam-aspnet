using FluentValidation;

namespace Exam.Clients.WebUI.Common.Models.Returnable.Actor
{
    public class ActorReturnModelValidator : AbstractValidator<ActorReturnModel>
    {
        public ActorReturnModelValidator()
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
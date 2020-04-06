using FluentValidation;

namespace Exam.Application.Storage.Actors.Commands.Delete
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(e => e.ActorId)
                .NotEmpty();
        }
    }
}
using FluentValidation;

namespace TechnicalTest.Application.Steps.Commands.CreateStep;

public class CreateStepCommandValidator : AbstractValidator<CreateStepCommand>
{
    public CreateStepCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}

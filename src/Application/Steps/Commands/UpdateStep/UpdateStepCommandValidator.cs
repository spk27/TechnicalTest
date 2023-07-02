using FluentValidation;

namespace TechnicalTest.Application.Steps.Commands.UpdateStep;

public class UpdateStepCommandValidator : AbstractValidator<UpdateStepCommand>
{
    public UpdateStepCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}

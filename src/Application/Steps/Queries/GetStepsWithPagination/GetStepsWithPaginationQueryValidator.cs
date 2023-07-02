using FluentValidation;

namespace TechnicalTest.Application.Steps.Queries.GetStepsWithPagination;

public class GetStepsWithPaginationQueryValidator : AbstractValidator<GetStepsWithPaginationQuery>
{
    public GetStepsWithPaginationQueryValidator()
    {
        RuleFor(x => x.ApplicantId)
            .NotEmpty().WithMessage("ApplicantId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

using TechnicalTest.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Application.Applicants.Commands.UpdateApplicant;

public class UpdateApplicantCommandValidator : AbstractValidator<UpdateApplicantCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateApplicantCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateApplicantCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Applicants
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

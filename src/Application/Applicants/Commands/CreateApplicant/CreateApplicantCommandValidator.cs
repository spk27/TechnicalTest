﻿using TechnicalTest.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Application.Applicants.Commands.CreateApplicant;

public class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateApplicantCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Applicants
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

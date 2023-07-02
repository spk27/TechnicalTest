using TechnicalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Applicant> Applicants { get; }

    DbSet<Step> Steps { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

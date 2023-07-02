using TechnicalTest.Application.Applicants.Queries.ExportApplicants;

namespace TechnicalTest.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildStepsFile(IEnumerable<StepRecord> records);
}

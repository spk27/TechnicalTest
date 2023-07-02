using System.Globalization;
using TechnicalTest.Application.Common.Interfaces;
using TechnicalTest.Application.Applicants.Queries.ExportApplicants;
using TechnicalTest.Infrastructure.Files.Maps;
using CsvHelper;

namespace TechnicalTest.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildStepsFile(IEnumerable<StepRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<StepRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}

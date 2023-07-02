using System.Globalization;
using TechnicalTest.Application.Applicants.Queries.ExportApplicants;
using CsvHelper.Configuration;

namespace TechnicalTest.Infrastructure.Files.Maps;

public class StepRecordMap : ClassMap<StepRecord>
{
    public StepRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}

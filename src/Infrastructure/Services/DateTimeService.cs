using TechnicalTest.Application.Common.Interfaces;

namespace TechnicalTest.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

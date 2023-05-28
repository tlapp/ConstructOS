using Construct.Application.Common.Interfaces;

namespace Construct.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}

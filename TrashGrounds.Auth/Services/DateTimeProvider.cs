using TrashGrounds.Auth.Services.Interfaces;

namespace TrashGrounds.Auth.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
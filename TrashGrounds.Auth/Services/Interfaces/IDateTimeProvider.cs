namespace TrashGrounds.Auth.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
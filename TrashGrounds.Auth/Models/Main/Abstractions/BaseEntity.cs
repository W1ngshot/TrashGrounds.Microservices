namespace TrashGrounds.Auth.Models.Main.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; protected init; } = Guid.NewGuid();
}
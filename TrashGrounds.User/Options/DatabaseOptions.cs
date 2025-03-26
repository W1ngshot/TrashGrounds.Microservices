namespace TrashGrounds.User.Options;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    public required string Host { get; init; }
    
    public int Port { get; init; }
    
    public required string Username { get; init; }
    
    public required string Password { get; init; }
    
    public required string Database { get; init; }
    
    public required string Schema { get; init; }
}
namespace TrashGrounds.Auth.Infrastructure.ValidationSetup;

public readonly struct ValidationBuilder<TBuilder>(TBuilder builder)
    where TBuilder : IEndpointConventionBuilder
{
    private readonly List<Type> _registrations = new();

    public ValidationBuilder<TBuilder> AddFor<TFor>()
    {
        _registrations.Add(typeof(TFor));
        return this;
    }

    public TBuilder SetValidation()
    {
        builder.AddValidationFilter(_registrations.ToArray());
        return builder;
    }
}

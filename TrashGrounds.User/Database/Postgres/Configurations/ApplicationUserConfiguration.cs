using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using TrashGrounds.User.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.User.Database.Postgres.Extensions;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Database.Postgres.Configurations;

public class ApplicationUserConfiguration : DependencyInjectedEntityConfiguration<AppUser>
{
    private readonly JsonOptions _jsonOptions;

    public ApplicationUserConfiguration(IOptions<JsonOptions> jsonOptions)
    {
        _jsonOptions = jsonOptions.Value;
    }
    
    public override void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(user => user.RefreshTokens)
            .HasJsonConversion<IReadOnlyList<RefreshToken>, List<RefreshToken>>(_jsonOptions.SerializerOptions);
    }
}
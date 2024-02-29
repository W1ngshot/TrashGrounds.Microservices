using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using TrashGrounds.Auth.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Auth.Database.Postgres.Extensions;
using TrashGrounds.Auth.Models.Main;

namespace TrashGrounds.Auth.Database.Postgres.Configurations;

public class AppUserConfiguration(IOptions<JsonOptions> jsonOptions) : DependencyInjectedEntityConfiguration<AppUser>
{
    private readonly JsonOptions _jsonOptions = jsonOptions.Value;

    public override void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.RefreshTokens)
            .HasJsonConversion<IReadOnlyList<RefreshToken>, List<RefreshToken>>(_jsonOptions.SerializerOptions);
    }
}
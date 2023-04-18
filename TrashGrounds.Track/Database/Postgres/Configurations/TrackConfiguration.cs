using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Track.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Track.Database.Postgres.Configurations;

public class TrackConfiguration : DependencyInjectedEntityConfiguration<Models.Main.Track>
{
    public override void Configure(EntityTypeBuilder<Models.Main.Track> builder)
    {
        builder.Property(track => track.Title)
            .IsRequired();

        builder.Property(track => track.ListensCount)
            .HasDefaultValue(0);

        builder.HasIndex(track => track.UserId);

        builder.HasMany(track => track.Genres)
            .WithMany(genre => genre.Tracks);
    }
}
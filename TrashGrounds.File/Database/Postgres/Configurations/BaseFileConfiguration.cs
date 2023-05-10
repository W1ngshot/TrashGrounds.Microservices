using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Template.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Template.Models.Main.Abstractions;

namespace TrashGrounds.Template.Database.Postgres.Configurations;

public abstract class BaseFileConfiguration<TEntity> : BaseConfiguration<TEntity>
    where TEntity : BaseFile
{
    public override void ConfigureChild(EntityTypeBuilder<TEntity> typeBuilder)
    {
        typeBuilder.Property(file => file.UploadDate)
            .HasDefaultValueSql("NOW()");
    }
}
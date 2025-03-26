﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrashGrounds.Track.Database.Postgres;

#nullable disable

namespace TrashGrounds.Track.Migrations
{
    [DbContext(typeof(TrackDbContext))]
    partial class TrackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("tracks")
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GenreMusicTrack", b =>
                {
                    b.Property<Guid>("GenresId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TracksId")
                        .HasColumnType("uuid");

                    b.HasKey("GenresId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("GenreMusicTrack", "tracks");
                });

            modelBuilder.Entity("TrashGrounds.Track.Models.Main.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres", "tracks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("21395468-d9de-4954-a1d9-8f8d4111f024"),
                            Name = "Русский рэп"
                        },
                        new
                        {
                            Id = new Guid("26f5682b-d20d-43e0-81fc-3c0a4bdc4fc3"),
                            Name = "Что-то странное"
                        },
                        new
                        {
                            Id = new Guid("c24b1a5a-431b-40a8-a3f9-b79573dd4e4f"),
                            Name = "Мэшап"
                        });
                });

            modelBuilder.Entity("TrashGrounds.Track.Models.Main.MusicTrack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsExplicit")
                        .HasColumnType("boolean");

                    b.Property<int>("ListensCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<Guid>("MusicId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PictureId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MusicTracks", "tracks");
                });

            modelBuilder.Entity("GenreMusicTrack", b =>
                {
                    b.HasOne("TrashGrounds.Track.Models.Main.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrashGrounds.Track.Models.Main.MusicTrack", null)
                        .WithMany()
                        .HasForeignKey("TracksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

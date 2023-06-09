﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrashGrounds.Track.Database.Postgres;

#nullable disable

namespace TrashGrounds.Track.Migrations
{
    [DbContext(typeof(TrackDbContext))]
    [Migration("20230510173914_LinksChangesToIds")]
    partial class LinksChangesToIds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
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

                    b.ToTable("GenreMusicTrack");
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

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d4b7bc34-3b01-4247-a9bc-02d4c5185e08"),
                            Name = "Русский рэп"
                        },
                        new
                        {
                            Id = new Guid("251ceebe-752a-4ea4-810e-03b8eaee7e5f"),
                            Name = "Что-то странное"
                        },
                        new
                        {
                            Id = new Guid("50e3aa21-ada0-42d3-a143-cf45922b97a5"),
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

                    b.ToTable("MusicTracks");
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

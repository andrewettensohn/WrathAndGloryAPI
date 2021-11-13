﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WrathAndGloryAPI.Data;

namespace WrathAndGloryAPI.Migrations
{
    [DbContext(typeof(WrathAndGloryContext))]
    partial class WrathAndGloryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("WrathAndGloryModels.RuleReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .HasColumnType("TEXT");

                    b.Property<int>("RuleReferenceType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecondaryBody")
                        .HasColumnType("TEXT");

                    b.Property<string>("SubTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RuleReferences");
                });

            modelBuilder.Entity("WrathAndGloryModels.SyncModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdateDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModelType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SyncModels");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Gol.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gol.Infrastructure.Migrations
{
    [DbContext(typeof(GolContext))]
    partial class GolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gol.Entities.Airplane", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AirplaneModel")
                        .HasColumnName("AirplaneModel")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("NumberOfPassengers")
                        .HasColumnName("NumberOfPassengers")
                        .HasColumnType("int")
                        .IsUnicode(false);

                    b.Property<DateTime>("RegistryCreationDate")
                        .HasColumnName("RegistryCreationDate")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.ToTable("Airplanes");
                });

            modelBuilder.Entity("Gol.Entities.Passenger", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AirplaneID")
                        .HasColumnName("AirplaneID");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<DateTime>("RegistryCreationDate")
                        .HasColumnName("RegistryCreationDate")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.HasIndex("AirplaneID");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("Gol.Entities.Passenger", b =>
                {
                    b.HasOne("Gol.Entities.Airplane", "Airplane")
                        .WithMany("Passengers")
                        .HasForeignKey("AirplaneID")
                        .HasConstraintName("FK__Passenger__AIRPLANE_I__1273C1CD");
                });
#pragma warning restore 612, 618
        }
    }
}

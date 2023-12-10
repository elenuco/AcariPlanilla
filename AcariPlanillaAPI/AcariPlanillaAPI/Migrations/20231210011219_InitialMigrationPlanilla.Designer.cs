﻿// <auto-generated />
using System;
using AcariPlanillaAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcariPlanillaAPI.Migrations
{
    [DbContext(typeof(PlanillaDbContext))]
    [Migration("20231210011219_InitialMigrationPlanilla")]
    partial class InitialMigrationPlanilla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcariPlanillaAPI.Models.Boletas", b =>
                {
                    b.Property<int>("BoletaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoletaId"));

                    b.Property<string>("CodigoEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Corte")
                        .HasColumnType("date");

                    b.Property<float>("DescuentoAFP")
                        .HasColumnType("real");

                    b.Property<float>("DescuentoISSS")
                        .HasColumnType("real");

                    b.Property<float>("DescuentoRenta")
                        .HasColumnType("real");

                    b.Property<float>("SueldoNeto")
                        .HasColumnType("real");

                    b.Property<int>("UsuariosUseId")
                        .HasColumnType("int");

                    b.HasKey("BoletaId");

                    b.HasIndex("UsuariosUseId");

                    b.ToTable("Boletas");
                });

            modelBuilder.Entity("AcariPlanillaAPI.Models.Usuarios", b =>
                {
                    b.Property<int>("UseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UseId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UseId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AcariPlanillaAPI.Models.Boletas", b =>
                {
                    b.HasOne("AcariPlanillaAPI.Models.Usuarios", "Usuarios")
                        .WithMany()
                        .HasForeignKey("UsuariosUseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
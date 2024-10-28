﻿// <auto-generated />
using System;
using Fit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fit.Migrations
{
    [DbContext(typeof(FitDbContext))]
    [Migration("20241028135200_updatewalking")]
    partial class updatewalking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fit.Models.Cycling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AverageSpeed")
                        .HasColumnType("double precision");

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<double>("TimeTaken")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cyclings");
                });

            modelBuilder.Entity("Fit.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CalorieTarget")
                        .HasColumnType("double precision");

                    b.Property<int?>("CyclingId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("HikingId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAchieved")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("RunningId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SwimmingId")
                        .HasColumnType("integer");

                    b.Property<double>("TotalCaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WalkingId")
                        .HasColumnType("integer");

                    b.Property<int?>("WeightsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CyclingId");

                    b.HasIndex("HikingId");

                    b.HasIndex("RunningId");

                    b.HasIndex("SwimmingId");

                    b.HasIndex("UserId");

                    b.HasIndex("WalkingId");

                    b.HasIndex("WeightsId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Fit.Models.Hiking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<double>("ElevationGain")
                        .HasColumnType("double precision");

                    b.Property<double>("TimeTaken")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Hikings");
                });

            modelBuilder.Entity("Fit.Models.Running", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AverageSpeed")
                        .HasColumnType("double precision");

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<double>("TimeTaken")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Runnings");
                });

            modelBuilder.Entity("Fit.Models.Swimming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AverageHeartRate")
                        .HasColumnType("double precision");

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<int>("Laps")
                        .HasColumnType("integer");

                    b.Property<double>("TimeTaken")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Swimmings");
                });

            modelBuilder.Entity("Fit.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fit.Models.Walking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<int>("Steps")
                        .HasColumnType("integer");

                    b.Property<double>("TimeTaken")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Walkings");
                });

            modelBuilder.Entity("Fit.Models.Weights", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CaloriesBurned")
                        .HasColumnType("double precision");

                    b.Property<int>("Repetitions")
                        .HasColumnType("integer");

                    b.Property<int>("Sets")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<double>("WeightLifted")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Weights");
                });

            modelBuilder.Entity("Fit.Models.Cycling", b =>
                {
                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Cyclings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit.Models.Goal", b =>
                {
                    b.HasOne("Fit.Models.Cycling", "Cycling")
                        .WithMany()
                        .HasForeignKey("CyclingId");

                    b.HasOne("Fit.Models.Hiking", "Hiking")
                        .WithMany()
                        .HasForeignKey("HikingId");

                    b.HasOne("Fit.Models.Running", "Running")
                        .WithMany()
                        .HasForeignKey("RunningId");

                    b.HasOne("Fit.Models.Swimming", "Swimming")
                        .WithMany()
                        .HasForeignKey("SwimmingId");

                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Goals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fit.Models.Walking", "Walking")
                        .WithMany()
                        .HasForeignKey("WalkingId");

                    b.HasOne("Fit.Models.Weights", "Weights")
                        .WithMany()
                        .HasForeignKey("WeightsId");

                    b.Navigation("Cycling");

                    b.Navigation("Hiking");

                    b.Navigation("Running");

                    b.Navigation("Swimming");

                    b.Navigation("User");

                    b.Navigation("Walking");

                    b.Navigation("Weights");
                });

            modelBuilder.Entity("Fit.Models.Hiking", b =>
                {
                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Hikings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit.Models.Running", b =>
                {
                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Runnings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit.Models.Swimming", b =>
                {
                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Swimmings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit.Models.Walking", b =>
                {
                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Walkings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit.Models.Weights", b =>
                {
                    b.HasOne("Fit.Models.User", "User")
                        .WithMany("Weights")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fit.Models.User", b =>
                {
                    b.Navigation("Cyclings");

                    b.Navigation("Goals");

                    b.Navigation("Hikings");

                    b.Navigation("Runnings");

                    b.Navigation("Swimmings");

                    b.Navigation("Walkings");

                    b.Navigation("Weights");
                });
#pragma warning restore 612, 618
        }
    }
}

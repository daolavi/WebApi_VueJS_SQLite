﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.Repository;

namespace WebApplication.Repository.Migrations
{
    [DbContext(typeof(WebApplicationDbContext))]
    partial class WebApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("WebApplication.Repository.Entities.HighScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.ToTable("HighScore");
                });

            modelBuilder.Entity("WebApplication.Repository.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .HasMaxLength(255);

                    b.Property<string>("Username")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
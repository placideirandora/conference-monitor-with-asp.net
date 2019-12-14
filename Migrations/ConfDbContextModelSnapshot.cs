﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_with_asp.net.Models;

namespace api_with_asp.net.Migrations
{
    [DbContext(typeof(ConfDbContext))]
    partial class ConfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("api_with_asp.net.Models.Conference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("EndDateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Organizer")
                        .HasColumnType("TEXT");

                    b.Property<string>("StartDateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Theme")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TicketPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Conferences");
                });
#pragma warning restore 612, 618
        }
    }
}

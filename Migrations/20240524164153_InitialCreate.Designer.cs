﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task.Data;

#nullable disable

namespace Task.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240524164153_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Task.Models.Node", b =>
                {
                    b.Property<int>("NodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NodeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentNodeId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("NodeId");

                    b.HasIndex("ParentNodeId");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("Task.Models.Node", b =>
                {
                    b.HasOne("Task.Models.Node", "ParentNode")
                        .WithMany("ChildNodes")
                        .HasForeignKey("ParentNodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentNode");
                });

            modelBuilder.Entity("Task.Models.Node", b =>
                {
                    b.Navigation("ChildNodes");
                });
#pragma warning restore 612, 618
        }
    }
}

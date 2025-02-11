﻿// <auto-generated />
using System;
using EAuction.DataAccessSqlite.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EAuction.API.Migrations
{
    [DbContext(typeof(DomainModelSqliteContext))]
    [Migration("20200110091321_sqliteMigration")]
    partial class sqliteMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("DomainModel.Model.DataEventRecord", b =>
                {
                    b.Property<long>("DataEventRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("SourceInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("DataEventRecordId");

                    b.HasIndex("SourceInfoId");

                    b.ToTable("DataEventRecords");
                });

            modelBuilder.Entity("DomainModel.Model.SourceInfo", b =>
                {
                    b.Property<long>("SourceInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("SourceInfoId");

                    b.ToTable("SourceInfos");
                });

            modelBuilder.Entity("DomainModel.Model.DataEventRecord", b =>
                {
                    b.HasOne("DomainModel.Model.SourceInfo", "SourceInfo")
                        .WithMany("DataEventRecords")
                        .HasForeignKey("SourceInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

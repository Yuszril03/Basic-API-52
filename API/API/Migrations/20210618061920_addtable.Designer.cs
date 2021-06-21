﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210618061920_addtable")]
    partial class addtable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIK");

                    b.ToTable("TB_T_Account");
                });

            modelBuilder.Entity("API.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GPA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profilingNIK")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("profilingNIK");

                    b.ToTable("TB_M_Education");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.ToTable("TB_M_Employee");
                });

            modelBuilder.Entity("API.Models.Profiling", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NIK");

                    b.ToTable("TB_T_Profiling");
                });

            modelBuilder.Entity("API.Models.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("educationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("educationId");

                    b.ToTable("TB_M_University");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.HasOne("API.Models.Employee", "employee")
                        .WithOne("account")
                        .HasForeignKey("API.Models.Account", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");
                });

            modelBuilder.Entity("API.Models.Education", b =>
                {
                    b.HasOne("API.Models.Profiling", "profiling")
                        .WithMany("Education_Id")
                        .HasForeignKey("profilingNIK");

                    b.Navigation("profiling");
                });

            modelBuilder.Entity("API.Models.Profiling", b =>
                {
                    b.HasOne("API.Models.Account", "account")
                        .WithOne("profiling")
                        .HasForeignKey("API.Models.Profiling", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("API.Models.University", b =>
                {
                    b.HasOne("API.Models.Education", "education")
                        .WithMany("University_Id")
                        .HasForeignKey("educationId");

                    b.Navigation("education");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Navigation("profiling");
                });

            modelBuilder.Entity("API.Models.Education", b =>
                {
                    b.Navigation("University_Id");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("account");
                });

            modelBuilder.Entity("API.Models.Profiling", b =>
                {
                    b.Navigation("Education_Id");
                });
#pragma warning restore 612, 618
        }
    }
}

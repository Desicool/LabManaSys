﻿// <auto-generated />
using System;
using DatabaseConnector.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseConnector.Migrations
{
    [DbContext(typeof(LabContext))]
    [Migration("20200326084740_Db_1_3")]
    partial class Db_1_3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.Budget", b =>
                {
                    b.Property<long>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.Property<string>("LabName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<double>("Used")
                        .HasColumnType("float");

                    b.HasKey("BudgetId");

                    b.HasIndex("LabId");

                    b.ToTable("Budget");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.Chemical", b =>
                {
                    b.Property<long>("ChemicalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("FactoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.Property<string>("LabName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime>("ProductionTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UnitMeasurement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.Property<long>("WorkFlowId")
                        .HasColumnType("bigint");

                    b.HasKey("ChemicalId");

                    b.HasIndex("LabId");

                    b.HasIndex("WorkFlowId");

                    b.ToTable("Chemical");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.NotificationMessage", b =>
                {
                    b.Property<long>("NotificationMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("FormId")
                        .HasColumnType("bigint");

                    b.Property<int>("FormType")
                        .HasColumnType("int");

                    b.Property<bool>("IsSolved")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("NotificationMessageId");

                    b.HasIndex("RoleId");

                    b.ToTable("NotificationMessage");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.Property<string>("LabName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.WorkFlow", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Applicant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkFlow");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.FormData.ClaimForm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Applicant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Approver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RealReturnTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LabId");

                    b.ToTable("ChaimForm");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.FormData.ClaimFormChemical", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChemicalId")
                        .HasColumnType("bigint");

                    b.Property<long>("ClaimFormId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChemicalId");

                    b.HasIndex("ClaimFormId");

                    b.ToTable("ClaimFormChemical");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.FormData.DeclarationForm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Applicant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WorkFlowId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LabId");

                    b.HasIndex("WorkFlowId");

                    b.ToTable("DeclarationForm");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.FormData.FinancialForm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Applicant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Receiver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WorkFlowId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LabId");

                    b.HasIndex("WorkFlowId");

                    b.ToTable("FinancialForm");
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.Chemical", b =>
                {
                    b.HasOne("DatabaseConnector.DAO.Entity.WorkFlow", null)
                        .WithMany("Chemicals")
                        .HasForeignKey("WorkFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.NotificationMessage", b =>
                {
                    b.HasOne("DatabaseConnector.DAO.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DatabaseConnector.DAO.Entity.UserRole", b =>
                {
                    b.HasOne("DatabaseConnector.DAO.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseConnector.DAO.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DatabaseConnector.DAO.FormData.ClaimFormChemical", b =>
                {
                    b.HasOne("DatabaseConnector.DAO.Entity.Chemical", "Chemical")
                        .WithMany()
                        .HasForeignKey("ChemicalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseConnector.DAO.FormData.ClaimForm", "ClaimForm")
                        .WithMany()
                        .HasForeignKey("ClaimFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

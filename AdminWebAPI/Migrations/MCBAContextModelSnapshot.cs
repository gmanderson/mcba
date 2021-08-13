﻿// <auto-generated />
using System;
using AdminWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdminWebAPI.Migrations
{
    [DbContext(typeof(MCBAContext))]
    partial class MCBAContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdminWebAPI.Models.Account", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("AccountNumber");

                    b.HasIndex("CustomerID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AdminWebAPI.Models.BillPay", b =>
                {
                    b.Property<int>("BillPayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<bool>("HasFailed")
                        .HasColumnType("bit");

                    b.Property<int>("PayeeID")
                        .HasColumnType("int");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateTime>("ScheduleTimeUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("BillPayID");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("PayeeID");

                    b.ToTable("BillPays");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Postcode")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("State")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Suburb")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("TFN")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Login", b =>
                {
                    b.Property<string>("LoginID")
                        .HasMaxLength(8)
                        .HasColumnType("nchar(8)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nchar(64)");

                    b.HasKey("LoginID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Payee", b =>
                {
                    b.Property<int>("PayeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("PayeeID");

                    b.ToTable("Payees");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Comment")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("DestinationAccountNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("DestinationAccountNumber");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Account", b =>
                {
                    b.HasOne("AdminWebAPI.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AdminWebAPI.Models.BillPay", b =>
                {
                    b.HasOne("AdminWebAPI.Models.Account", "Account")
                        .WithMany("BillPays")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminWebAPI.Models.Payee", "Payee")
                        .WithMany("BillPays")
                        .HasForeignKey("PayeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Payee");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Login", b =>
                {
                    b.HasOne("AdminWebAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Transaction", b =>
                {
                    b.HasOne("AdminWebAPI.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminWebAPI.Models.Account", "DestinationAccount")
                        .WithMany()
                        .HasForeignKey("DestinationAccountNumber");

                    b.Navigation("Account");

                    b.Navigation("DestinationAccount");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Account", b =>
                {
                    b.Navigation("BillPays");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Customer", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("AdminWebAPI.Models.Payee", b =>
                {
                    b.Navigation("BillPays");
                });
#pragma warning restore 612, 618
        }
    }
}

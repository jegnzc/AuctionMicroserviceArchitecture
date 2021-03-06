// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentService.Models;

namespace PaymentService.Migrations
{
    [DbContext(typeof(PaymentServiceContext))]
    [Migration("20191007144745_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaymentService.Models.AuctionPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BidUser");

                    b.Property<string>("CreditCardNo");

                    b.Property<int>("IdAuction");

                    b.Property<int>("Month");

                    b.Property<string>("Name");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<int>("PaymentStatus");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("AuctionPayment");
                });
#pragma warning restore 612, 618
        }
    }
}

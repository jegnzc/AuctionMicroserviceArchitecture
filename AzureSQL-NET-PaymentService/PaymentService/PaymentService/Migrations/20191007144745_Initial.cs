using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditCardNo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IdAuction = table.Column<int>(nullable: false),
                    BidUser = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionPayment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionPayment");
        }
    }
}

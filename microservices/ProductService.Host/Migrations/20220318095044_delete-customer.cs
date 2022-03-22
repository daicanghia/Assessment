using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Host.Migrations
{
    public partial class deletecustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PmHistoryActions_PmCustomers_CustomerId",
                table: "PmHistoryActions");

            migrationBuilder.DropTable(
                name: "PmCustomers");

            migrationBuilder.DropIndex(
                name: "IX_PmHistoryActions_CustomerId",
                table: "PmHistoryActions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PmCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmCustomers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PmHistoryActions_CustomerId",
                table: "PmHistoryActions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PmHistoryActions_PmCustomers_CustomerId",
                table: "PmHistoryActions",
                column: "CustomerId",
                principalTable: "PmCustomers",
                principalColumn: "Id");
        }
    }
}

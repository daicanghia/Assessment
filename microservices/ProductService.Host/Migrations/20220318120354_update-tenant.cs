using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Host.Migrations
{
    public partial class updatetenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "PmHistoryActions",
                newName: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "PmHistoryActions",
                newName: "CustomerId");
        }
    }
}

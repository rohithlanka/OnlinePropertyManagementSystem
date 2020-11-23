using Microsoft.EntityFrameworkCore.Migrations;

namespace ListService.Migrations
{
    public partial class namech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Buildings",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Buildings",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Buildings",
                newName: "address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Buildings",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "Buildings",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Buildings",
                newName: "Address");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace upload_pptx.Migrations
{
    public partial class migd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "slides",
                newName: "Yol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Yol",
                table: "slides",
                newName: "Path");
        }
    }
}

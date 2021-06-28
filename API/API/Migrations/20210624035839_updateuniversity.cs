using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateuniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TB_M_University",
                newName: "UniversityName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniversityName",
                table: "TB_M_University",
                newName: "Name");
        }
    }
}

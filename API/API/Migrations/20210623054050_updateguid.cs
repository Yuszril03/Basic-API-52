using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "TB_T_Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "TB_T_Account");
        }
    }
}

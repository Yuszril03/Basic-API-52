using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateTablee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_educationId",
                table: "TB_T_Profiling");

            migrationBuilder.AlterColumn<int>(
                name: "educationId",
                table: "TB_T_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_educationId",
                table: "TB_T_Profiling",
                column: "educationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_educationId",
                table: "TB_T_Profiling");

            migrationBuilder.AlterColumn<int>(
                name: "educationId",
                table: "TB_T_Profiling",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_educationId",
                table: "TB_T_Profiling",
                column: "educationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

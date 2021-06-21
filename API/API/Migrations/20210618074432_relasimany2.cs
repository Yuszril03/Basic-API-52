using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class relasimany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_T_Profiling_profilingNIK",
                table: "TB_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_University_TB_M_Education_educationId",
                table: "TB_M_University");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_University_educationId",
                table: "TB_M_University");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_profilingNIK",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "educationId",
                table: "TB_M_University");

            migrationBuilder.DropColumn(
                name: "profilingNIK",
                table: "TB_M_Education");

            migrationBuilder.AddColumn<int>(
                name: "Education_Id",
                table: "TB_T_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "educationId",
                table: "TB_T_Profiling",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "University_Id",
                table: "TB_M_Education",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "universityId",
                table: "TB_M_Education",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Profiling_educationId",
                table: "TB_T_Profiling",
                column: "educationId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_universityId",
                table: "TB_M_Education",
                column: "universityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_M_University_universityId",
                table: "TB_M_Education",
                column: "universityId",
                principalTable: "TB_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_educationId",
                table: "TB_T_Profiling",
                column: "educationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_universityId",
                table: "TB_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Profiling_TB_M_Education_educationId",
                table: "TB_T_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Profiling_educationId",
                table: "TB_T_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_universityId",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "Education_Id",
                table: "TB_T_Profiling");

            migrationBuilder.DropColumn(
                name: "educationId",
                table: "TB_T_Profiling");

            migrationBuilder.DropColumn(
                name: "University_Id",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "universityId",
                table: "TB_M_Education");

            migrationBuilder.AddColumn<int>(
                name: "educationId",
                table: "TB_M_University",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profilingNIK",
                table: "TB_M_Education",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_University_educationId",
                table: "TB_M_University",
                column: "educationId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_profilingNIK",
                table: "TB_M_Education",
                column: "profilingNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_T_Profiling_profilingNIK",
                table: "TB_M_Education",
                column: "profilingNIK",
                principalTable: "TB_T_Profiling",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_University_TB_M_Education_educationId",
                table: "TB_M_University",
                column: "educationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

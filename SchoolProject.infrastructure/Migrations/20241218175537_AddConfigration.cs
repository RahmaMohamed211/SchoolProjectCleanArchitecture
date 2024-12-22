using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DId",
                table: "Students",
                column: "DId",
                principalTable: "Departments",
                principalColumn: "DId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DId",
                table: "Students",
                column: "DId",
                principalTable: "Departments",
                principalColumn: "DId");
        }
    }
}

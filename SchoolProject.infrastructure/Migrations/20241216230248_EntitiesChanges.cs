using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "Subjects",
                newName: "SubjectNameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "DName",
                table: "Departments",
                newName: "DNameAr");

            migrationBuilder.AddColumn<int>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "int",
                maxLength: 500,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNameEn",
                table: "Departments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectNameAr",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DNameEn",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "SubjectNameEn",
                table: "Subjects",
                newName: "SubjectName");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DNameAr",
                table: "Departments",
                newName: "DName");
        }
    }
}

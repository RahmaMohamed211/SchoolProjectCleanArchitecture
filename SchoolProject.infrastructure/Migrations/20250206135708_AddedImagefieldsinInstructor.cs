using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedImagefieldsinInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Instructor",
                type: "nvarchar(max)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Instructor");
        }
    }
}

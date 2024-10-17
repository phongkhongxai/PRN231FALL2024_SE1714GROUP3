using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class updateDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificationsJson",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInfoJson",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationJson",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExperiencesJson",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectsJson",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillsJson",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificationsJson",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ContactInfoJson",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "EducationJson",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ExperiencesJson",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ProjectsJson",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "SkillsJson",
                table: "Resumes");
        }
    }
}

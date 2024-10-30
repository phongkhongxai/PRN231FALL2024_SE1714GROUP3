using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class updatestatusround : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InterviewRounds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "InterviewRounds");
        }
    }
}

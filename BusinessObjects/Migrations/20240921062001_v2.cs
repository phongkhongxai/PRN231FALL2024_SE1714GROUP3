using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Users_UserId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_UserId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Interviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Interviews",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_UserId",
                table: "Interviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Users_UserId",
                table: "Interviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

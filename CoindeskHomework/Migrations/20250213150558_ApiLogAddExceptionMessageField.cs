using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoindeskHomework.Migrations
{
    /// <inheritdoc />
    public partial class ApiLogAddExceptionMessageField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExceptionMessage",
                table: "ApiLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionMessage",
                table: "ApiLogs");
        }
    }
}

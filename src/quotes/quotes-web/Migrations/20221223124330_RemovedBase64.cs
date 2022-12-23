using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quotesweb.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Data",
                table: "File");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Data",
                table: "File",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

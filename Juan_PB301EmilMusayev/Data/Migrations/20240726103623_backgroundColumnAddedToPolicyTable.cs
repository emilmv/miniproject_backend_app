using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juan_PB301EmilMusayev.Data.Migrations
{
    /// <inheritdoc />
    public partial class backgroundColumnAddedToPolicyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Policies");
        }
    }
}

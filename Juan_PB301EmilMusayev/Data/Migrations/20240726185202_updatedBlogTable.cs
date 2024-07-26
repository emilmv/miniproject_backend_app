using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juan_PB301EmilMusayev.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Blogs");
        }
    }
}

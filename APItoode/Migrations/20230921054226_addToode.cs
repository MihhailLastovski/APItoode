using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APItoode.Migrations
{
    /// <inheritdoc />
    public partial class addToode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kogus",
                table: "Tooted",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kogus",
                table: "Tooted");
        }
    }
}

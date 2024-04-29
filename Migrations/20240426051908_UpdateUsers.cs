using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "CCCD",
            table: "Users",
            type: "nvarchar(12)",
            maxLength: 12,
            nullable: false,
            defaultValue: "")
            .Annotation("Relational:ColumnOrder", 2);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "Users");
        }
    }
}

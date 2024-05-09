using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDChuCoSo",
                table: "CoSo");

            migrationBuilder.AddColumn<string>(
                name: "NoiDung",
                table: "BaoCaoViPham",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiDung",
                table: "BaoCaoViPham");

            migrationBuilder.AddColumn<int>(
                name: "IDChuCoSo",
                table: "CoSo",
                type: "int",
                nullable: true);
        }
    }
}

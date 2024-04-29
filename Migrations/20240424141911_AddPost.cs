using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinTuc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    IDTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCanBo = table.Column<int>(type: "int", nullable: true),
                    NgayDang = table.Column<DateOnly>(type: "date", nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TinTuc__74C0F8F80B49A437", x => x.IDTinTuc);
                    table.ForeignKey(
                        name: "FK__TinTuc__IDCanBo__4D94879B",
                        column: x => x.IDCanBo,
                        principalTable: "CanBo",
                        principalColumn: "IDCanBo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_IDCanBo",
                table: "TinTuc",
                column: "IDCanBo");
        }
    }
}

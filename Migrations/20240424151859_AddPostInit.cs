using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddPostInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DanhMuc_Slug",
                table: "DanhMuc");

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    IDTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    IDCanBo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTuc", x => x.IDTinTuc);
                    table.ForeignKey(
                        name: "FK_TinTuc_Users_IDCanBo",
                        column: x => x.IDCanBo,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucBaiDang",
                columns: table => new
                {
                    IDBaiDang = table.Column<int>(type: "int", nullable: false),
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucBaiDang", x => new { x.IDBaiDang, x.IDDanhMuc });
                    table.ForeignKey(
                        name: "FK_DanhMucBaiDang_DanhMuc_IDDanhMuc",
                        column: x => x.IDDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhMucBaiDang_TinTuc_IDBaiDang",
                        column: x => x.IDBaiDang,
                        principalTable: "TinTuc",
                        principalColumn: "IDTinTuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhMuc_Slug",
                table: "DanhMuc",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucBaiDang_IDDanhMuc",
                table: "DanhMucBaiDang",
                column: "IDDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_IDCanBo",
                table: "TinTuc",
                column: "IDCanBo");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_Slug",
                table: "TinTuc",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMucBaiDang");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropIndex(
                name: "IX_DanhMuc_Slug",
                table: "DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMuc_Slug",
                table: "DanhMuc",
                column: "Slug");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhMuc_DanhMuc_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "DanhMuc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhMuc_ParentCategoryId",
                table: "DanhMuc",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMuc_Slug",
                table: "DanhMuc",
                column: "Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChuCoSo",
                columns: table => new
                {
                    IDChuCoSo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    SDT = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChuCoSo__00A8457314C153D6", x => x.IDChuCoSo);
                });

            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    IDChucVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChucVu__70FCCF652716418C", x => x.IDChucVu);
                });

            migrationBuilder.CreateTable(
                name: "KeHoach",
                columns: table => new
                {
                    IDKeHoach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    DoanSo = table.Column<int>(type: "int", nullable: true),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KeHoach__936F11C8A85196CC", x => x.IDKeHoach);
                });

            migrationBuilder.CreateTable(
                name: "MucKiemTra",
                columns: table => new
                {
                    IDMucKT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MucKiemT__2452E27ECB438FBA", x => x.IDMucKT);
                });

            migrationBuilder.CreateTable(
                name: "CoSo",
                columns: table => new
                {
                    IDCoSo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDChuCoSo = table.Column<int>(type: "int", nullable: true),
                    TenCoSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiHinhKinhDoanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoGiayPhepKD = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NgayCapGiayPhepKD = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayCapCNATTP = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayHetHanCNATTP = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CoSo__344441C5716816E6", x => x.IDCoSo);
                    table.ForeignKey(
                        name: "FK__CoSo__IDChuCoSo__3E52440B",
                        column: x => x.IDChuCoSo,
                        principalTable: "ChuCoSo",
                        principalColumn: "IDChuCoSo");
                });

            migrationBuilder.CreateTable(
                name: "CanBo",
                columns: table => new
                {
                    IDCanBo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IDChucVu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CanBo__D8C385AC7420D539", x => x.IDCanBo);
                    table.ForeignKey(
                        name: "FK__CanBo__IDChucVu__398D8EEE",
                        column: x => x.IDChucVu,
                        principalTable: "ChucVu",
                        principalColumn: "IDChucVu");
                });

            migrationBuilder.CreateTable(
                name: "BanCongBoSP",
                columns: table => new
                {
                    IDBanCongBoSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCoSo = table.Column<int>(type: "int", nullable: true),
                    NgayCongBo = table.Column<DateOnly>(type: "date", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhPhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiHanSuDung = table.Column<DateOnly>(type: "date", nullable: true),
                    CachDongGoi_BaoBi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_DiaChiSX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MauNhanSP = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    HinhAnhMinhChung = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BanCongB__57824418B2AEB7B3", x => x.IDBanCongBoSP);
                    table.ForeignKey(
                        name: "FK__BanCongBo__IDCoS__440B1D61",
                        column: x => x.IDCoSo,
                        principalTable: "CoSo",
                        principalColumn: "IDCoSo");
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoViPham",
                columns: table => new
                {
                    IDBaoCao = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    CCCD = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    NgayBaoCao = table.Column<DateOnly>(type: "date", nullable: true),
                    HinhAnhMinhChung = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IDCoSo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BaoCaoVi__BC216EF0A853FDD6", x => x.IDBaoCao);
                    table.ForeignKey(
                        name: "FK__BaoCaoViP__IDCoS__4AB81AF0",
                        column: x => x.IDCoSo,
                        principalTable: "CoSo",
                        principalColumn: "IDCoSo");
                });

            migrationBuilder.CreateTable(
                name: "HoSoCapGiayChungNhan",
                columns: table => new
                {
                    IDGiayChungNhan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCoSo = table.Column<int>(type: "int", nullable: true),
                    NgayDangKy = table.Column<DateOnly>(type: "date", nullable: true),
                    LoaiThucPham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnhMinhChung = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoSoCapG__729C7BB617160CD8", x => x.IDGiayChungNhan);
                    table.ForeignKey(
                        name: "FK__HoSoCapGi__IDCoS__412EB0B6",
                        column: x => x.IDCoSo,
                        principalTable: "CoSo",
                        principalColumn: "IDCoSo");
                });

            migrationBuilder.CreateTable(
                name: "KeHoach_CoSo",
                columns: table => new
                {
                    IDKeHoachCoSo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDKeHoach = table.Column<int>(type: "int", nullable: true),
                    IDCoSo = table.Column<int>(type: "int", nullable: true),
                    ThoiGianKiemTra = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayTao = table.Column<DateOnly>(type: "date", nullable: true),
                    KetLuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhacPhuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YKienChuCoSo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KeHoach___146E827EF1EC716F", x => x.IDKeHoachCoSo);
                    table.ForeignKey(
                        name: "FK__KeHoach_C__IDCoS__571DF1D5",
                        column: x => x.IDCoSo,
                        principalTable: "CoSo",
                        principalColumn: "IDCoSo");
                    table.ForeignKey(
                        name: "FK__KeHoach_C__IDKeH__5629CD9C",
                        column: x => x.IDKeHoach,
                        principalTable: "KeHoach",
                        principalColumn: "IDKeHoach");
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoThayDoi",
                columns: table => new
                {
                    IDThongBao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCoSo = table.Column<int>(type: "int", nullable: true),
                    IDChuCoSoMoi = table.Column<int>(type: "int", nullable: true),
                    TenCoSoMoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiMoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThongBao__3EBBFAAEC5198DAB", x => x.IDThongBao);
                    table.ForeignKey(
                        name: "FK__ThongBaoT__IDChu__47DBAE45",
                        column: x => x.IDChuCoSoMoi,
                        principalTable: "ChuCoSo",
                        principalColumn: "IDChuCoSo");
                    table.ForeignKey(
                        name: "FK__ThongBaoT__IDCoS__46E78A0C",
                        column: x => x.IDCoSo,
                        principalTable: "CoSo",
                        principalColumn: "IDCoSo");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDoanThanhTra",
                columns: table => new
                {
                    IDKeHoach = table.Column<int>(type: "int", nullable: false),
                    IDCanBo = table.Column<int>(type: "int", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__4EE32992008214FC", x => new { x.IDKeHoach, x.IDCanBo });
                    table.ForeignKey(
                        name: "FK__ChiTietDo__IDCan__534D60F1",
                        column: x => x.IDCanBo,
                        principalTable: "CanBo",
                        principalColumn: "IDCanBo");
                    table.ForeignKey(
                        name: "FK__ChiTietDo__IDKeH__52593CB8",
                        column: x => x.IDKeHoach,
                        principalTable: "KeHoach",
                        principalColumn: "IDKeHoach");
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    IDTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    NgayDang = table.Column<DateOnly>(type: "date", nullable: true),
                    IDCanBo = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ChiTietKetQua",
                columns: table => new
                {
                    IDKeHoachCoSo = table.Column<int>(type: "int", nullable: false),
                    IDMucKT = table.Column<int>(type: "int", nullable: false),
                    Dat = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietK__E62BAC593B114B18", x => new { x.IDKeHoachCoSo, x.IDMucKT });
                    table.ForeignKey(
                        name: "FK__ChiTietKe__IDKeH__5BE2A6F2",
                        column: x => x.IDKeHoachCoSo,
                        principalTable: "KeHoach_CoSo",
                        principalColumn: "IDKeHoachCoSo");
                    table.ForeignKey(
                        name: "FK__ChiTietKe__IDMuc__5CD6CB2B",
                        column: x => x.IDMucKT,
                        principalTable: "MucKiemTra",
                        principalColumn: "IDMucKT");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BanCongBoSP_IDCoSo",
                table: "BanCongBoSP",
                column: "IDCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoViPham_IDCoSo",
                table: "BaoCaoViPham",
                column: "IDCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_CanBo_IDChucVu",
                table: "CanBo",
                column: "IDChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDoanThanhTra_IDCanBo",
                table: "ChiTietDoanThanhTra",
                column: "IDCanBo");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKetQua_IDMucKT",
                table: "ChiTietKetQua",
                column: "IDMucKT");

            migrationBuilder.CreateIndex(
                name: "IX_CoSo_IDChuCoSo",
                table: "CoSo",
                column: "IDChuCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoCapGiayChungNhan_IDCoSo",
                table: "HoSoCapGiayChungNhan",
                column: "IDCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_KeHoach_CoSo_IDCoSo",
                table: "KeHoach_CoSo",
                column: "IDCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_KeHoach_CoSo_IDKeHoach",
                table: "KeHoach_CoSo",
                column: "IDKeHoach");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoThayDoi_IDChuCoSoMoi",
                table: "ThongBaoThayDoi",
                column: "IDChuCoSoMoi");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoThayDoi_IDCoSo",
                table: "ThongBaoThayDoi",
                column: "IDCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_IDCanBo",
                table: "TinTuc",
                column: "IDCanBo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanCongBoSP");

            migrationBuilder.DropTable(
                name: "BaoCaoViPham");

            migrationBuilder.DropTable(
                name: "ChiTietDoanThanhTra");

            migrationBuilder.DropTable(
                name: "ChiTietKetQua");

            migrationBuilder.DropTable(
                name: "HoSoCapGiayChungNhan");

            migrationBuilder.DropTable(
                name: "ThongBaoThayDoi");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropTable(
                name: "KeHoach_CoSo");

            migrationBuilder.DropTable(
                name: "MucKiemTra");

            migrationBuilder.DropTable(
                name: "CanBo");

            migrationBuilder.DropTable(
                name: "CoSo");

            migrationBuilder.DropTable(
                name: "KeHoach");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "ChuCoSo");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAnToanVeSinhThucPhamDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "LienHe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LienHe", x => x.Id);
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
                name: "QuanHuyen",
                columns: table => new
                {
                    IDQuanHuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuanHuyen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanHuyen", x => x.IDQuanHuyen);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    HomeAdress = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhuongXa",
                columns: table => new
                {
                    IDPhuongXa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhuongXa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IDQuanHuyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongXa", x => x.IDPhuongXa);
                    table.ForeignKey(
                        name: "FK_PhuongXa_QuanHuyen_IDQuanHuyen",
                        column: x => x.IDQuanHuyen,
                        principalTable: "QuanHuyen",
                        principalColumn: "IDQuanHuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDoanThanhTra",
                columns: table => new
                {
                    IDKeHoach = table.Column<int>(type: "int", nullable: false),
                    IDCanBo = table.Column<int>(type: "int", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanBoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__4EE32992008214FC", x => new { x.IDKeHoach, x.IDCanBo });
                    table.ForeignKey(
                        name: "FK_ChiTietDoanThanhTra_Users_CanBoId",
                        column: x => x.CanBoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    IDPhuongXa = table.Column<int>(type: "int", nullable: true),
                    LoaiHinhKinhDoanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoGiayPhepKD = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NgayCapGiayPhepKD = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayCapCNATTP = table.Column<DateOnly>(type: "date", nullable: true),
                    NgayHetHanCNATTP = table.Column<DateOnly>(type: "date", nullable: true),
                    ChuCoSoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CoSo__344441C5716816E6", x => x.IDCoSo);
                    table.ForeignKey(
                        name: "FK_CoSo_PhuongXa_IDPhuongXa",
                        column: x => x.IDPhuongXa,
                        principalTable: "PhuongXa",
                        principalColumn: "IDPhuongXa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoSo_Users_ChuCoSoId",
                        column: x => x.ChuCoSoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    ChuCoSoMoiId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThongBao__3EBBFAAEC5198DAB", x => x.IDThongBao);
                    table.ForeignKey(
                        name: "FK_ThongBaoThayDoi_Users_ChuCoSoMoiId",
                        column: x => x.ChuCoSoMoiId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ThongBaoT__IDCoS__46E78A0C",
                        column: x => x.IDCoSo,
                        principalTable: "CoSo",
                        principalColumn: "IDCoSo");
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
                name: "IX_ChiTietDoanThanhTra_CanBoId",
                table: "ChiTietDoanThanhTra",
                column: "CanBoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKetQua_IDMucKT",
                table: "ChiTietKetQua",
                column: "IDMucKT");

            migrationBuilder.CreateIndex(
                name: "IX_CoSo_ChuCoSoId",
                table: "CoSo",
                column: "ChuCoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_CoSo_IDPhuongXa",
                table: "CoSo",
                column: "IDPhuongXa");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMuc_ParentCategoryId",
                table: "DanhMuc",
                column: "ParentCategoryId");

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
                name: "IX_PhuongXa_IDQuanHuyen",
                table: "PhuongXa",
                column: "IDQuanHuyen");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoThayDoi_ChuCoSoMoiId",
                table: "ThongBaoThayDoi",
                column: "ChuCoSoMoiId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoThayDoi_IDCoSo",
                table: "ThongBaoThayDoi",
                column: "IDCoSo");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_IDCanBo",
                table: "TinTuc",
                column: "IDCanBo");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_Slug",
                table: "TinTuc",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
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
                name: "DanhMucBaiDang");

            migrationBuilder.DropTable(
                name: "HoSoCapGiayChungNhan");

            migrationBuilder.DropTable(
                name: "LienHe");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "ThongBaoThayDoi");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "KeHoach_CoSo");

            migrationBuilder.DropTable(
                name: "MucKiemTra");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CoSo");

            migrationBuilder.DropTable(
                name: "KeHoach");

            migrationBuilder.DropTable(
                name: "PhuongXa");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "QuanHuyen");
        }
    }
}

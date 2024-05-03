using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class AtvstpContext : DbContext
{
    public AtvstpContext()
    {
    }

    public AtvstpContext(DbContextOptions<AtvstpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BanCongBoSp> BanCongBoSps { get; set; }

    public virtual DbSet<BaoCaoViPham> BaoCaoViPhams { get; set; }

    public virtual DbSet<ChiTietDoanThanhTra> ChiTietDoanThanhTras { get; set; }

    public virtual DbSet<ChiTietKetQua> ChiTietKetQuas { get; set; }

    public virtual DbSet<CoSo> CoSos { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<HoSoCapGiayChungNhan> HoSoCapGiayChungNhans { get; set; }

    public virtual DbSet<KeHoach> KeHoaches { get; set; }

    public virtual DbSet<KeHoachCoSo> KeHoachCoSos { get; set; }

    public virtual DbSet<MucKiemTra> MucKiemTras { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhuongXa> PhuongXas { get; set; }

    public virtual DbSet<QuanHuyen> QuanHuyens { get; set; }

    public virtual DbSet<ThongBaoThayDoi> ThongBaoThayDois { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    public virtual DbSet<XacThucNguoiDung> XacThucNguoiDungs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LI8202B;Initial Catalog=ATVSTP;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanCongBoSp>(entity =>
        {
            entity.HasKey(e => e.IdbanCongBoSp).HasName("PK__BanCongB__57824418118041B7");

            entity.ToTable("BanCongBoSP");

            entity.Property(e => e.IdbanCongBoSp).HasColumnName("IDBanCongBoSP");
            entity.Property(e => e.CachDongGoiBaoBi).HasColumnName("CachDongGoi_BaoBi");
            entity.Property(e => e.HinhAnhMinhChung).IsUnicode(false);
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.MauNhanSp)
                .IsUnicode(false)
                .HasColumnName("MauNhanSP");
            entity.Property(e => e.TenDiaChiSx).HasColumnName("Ten_DiaChiSX");

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.BanCongBoSps)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__BanCongBo__IDCoS__4F7CD00D");
        });

        modelBuilder.Entity<BaoCaoViPham>(entity =>
        {
            entity.HasKey(e => e.IdbaoCao).HasName("PK__BaoCaoVi__BC216EF02B3716F3");

            entity.ToTable("BaoCaoViPham");

            entity.Property(e => e.IdbaoCao).HasColumnName("IDBaoCao");
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.HinhAnhMinhChung).IsUnicode(false);
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDT");

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.BaoCaoViPhams)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__BaoCaoViP__IDCoS__5629CD9C");
        });

        modelBuilder.Entity<ChiTietDoanThanhTra>(entity =>
        {
            entity.HasKey(e => new { e.IdkeHoach, e.IdcanBo }).HasName("PK__ChiTietD__4EE32992DA1C22C2");

            entity.ToTable("ChiTietDoanThanhTra");

            entity.Property(e => e.IdkeHoach).HasColumnName("IDKeHoach");
            entity.Property(e => e.IdcanBo)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasColumnName("IDCanBo");

            entity.HasOne(d => d.IdcanBoNavigation).WithMany(p => p.ChiTietDoanThanhTras)
                .HasForeignKey(d => d.IdcanBo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__IDCan__656C112C");

            entity.HasOne(d => d.IdkeHoachNavigation).WithMany(p => p.ChiTietDoanThanhTras)
                .HasForeignKey(d => d.IdkeHoach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__IDKeH__6477ECF3");
        });

        modelBuilder.Entity<ChiTietKetQua>(entity =>
        {
            entity.HasKey(e => new { e.IdkeHoachCoSo, e.IdmucKt }).HasName("PK__ChiTietK__E62BAC592D6FDD56");

            entity.ToTable("ChiTietKetQua");

            entity.Property(e => e.IdkeHoachCoSo).HasColumnName("IDKeHoachCoSo");
            entity.Property(e => e.IdmucKt).HasColumnName("IDMucKT");

            entity.HasOne(d => d.IdkeHoachCoSoNavigation).WithMany(p => p.ChiTietKetQuas)
                .HasForeignKey(d => d.IdkeHoachCoSo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietKe__IDKeH__6E01572D");

            entity.HasOne(d => d.IdmucKtNavigation).WithMany(p => p.ChiTietKetQuas)
                .HasForeignKey(d => d.IdmucKt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietKe__IDMuc__6EF57B66");
        });

        modelBuilder.Entity<CoSo>(entity =>
        {
            entity.HasKey(e => e.IdcoSo).HasName("PK__CoSo__344441C53984D5C8");

            entity.ToTable("CoSo");

            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.IdchuCoSo)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasColumnName("IDChuCoSo");
            entity.Property(e => e.IdphuongXa).HasColumnName("IDPhuongXa");
            entity.Property(e => e.NgayCapCnattp).HasColumnName("NgayCapCNATTP");
            entity.Property(e => e.NgayCapGiayPhepKd).HasColumnName("NgayCapGiayPhepKD");
            entity.Property(e => e.NgayHetHanCnattp).HasColumnName("NgayHetHanCNATTP");
            entity.Property(e => e.SoGiayPhepKd)
                .IsUnicode(false)
                .HasColumnName("SoGiayPhepKD");

            entity.HasOne(d => d.IdchuCoSoNavigation).WithMany(p => p.CoSos)
                .HasForeignKey(d => d.IdchuCoSo)
                .HasConstraintName("FK__CoSo__IDChuCoSo__46E78A0C");

            entity.HasOne(d => d.IdphuongXaNavigation).WithMany(p => p.CoSos)
                .HasForeignKey(d => d.IdphuongXa)
                .HasConstraintName("FK__CoSo__IDPhuongXa__47DBAE45");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DanhMuc__3214EC07E6367CA7");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.Slug).HasMaxLength(100);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);

            entity.HasOne(d => d.IdDanhMucChaNavigation).WithMany(p => p.InverseIdDanhMucChaNavigation)
                .HasForeignKey(d => d.IdDanhMucCha)
                .HasConstraintName("FK__DanhMuc__IdDanhM__5BE2A6F2");
        });

        modelBuilder.Entity<HoSoCapGiayChungNhan>(entity =>
        {
            entity.HasKey(e => e.IdgiayChungNhan).HasName("PK__HoSoCapG__729C7BB6C88021F7");

            entity.ToTable("HoSoCapGiayChungNhan");

            entity.Property(e => e.IdgiayChungNhan).HasColumnName("IDGiayChungNhan");
            entity.Property(e => e.HinhAnhMinhChung).IsUnicode(false);
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.NgayDangKy).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasDefaultValue(0);

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.HoSoCapGiayChungNhans)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__HoSoCapGi__IDCoS__4CA06362");
        });

        modelBuilder.Entity<KeHoach>(entity =>
        {
            entity.HasKey(e => e.IdkeHoach).HasName("PK__KeHoach__936F11C84AFD428F");

            entity.ToTable("KeHoach");

            entity.Property(e => e.IdkeHoach).HasColumnName("IDKeHoach");
            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
        });

        modelBuilder.Entity<KeHoachCoSo>(entity =>
        {
            entity.HasKey(e => e.IdkeHoachCoSo).HasName("PK__KeHoach___146E827E8E5C7607");

            entity.ToTable("KeHoach_CoSo");

            entity.Property(e => e.IdkeHoachCoSo).HasColumnName("IDKeHoachCoSo");
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.IdkeHoach).HasColumnName("IDKeHoach");
            entity.Property(e => e.ThoiGianKiemTra).HasColumnType("datetime");
            entity.Property(e => e.YkienChuCoSo).HasColumnName("YKienChuCoSo");

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.KeHoachCoSos)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__KeHoach_C__IDCoS__693CA210");

            entity.HasOne(d => d.IdkeHoachNavigation).WithMany(p => p.KeHoachCoSos)
                .HasForeignKey(d => d.IdkeHoach)
                .HasConstraintName("FK__KeHoach_C__IDKeH__68487DD7");
        });

        modelBuilder.Entity<MucKiemTra>(entity =>
        {
            entity.HasKey(e => e.IdmucKt).HasName("PK__MucKiemT__2452E27E43257A9D");

            entity.ToTable("MucKiemTra");

            entity.Property(e => e.IdmucKt).HasColumnName("IDMucKT");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC07294956E0");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.DiaChiNha).HasMaxLength(400);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmailChuanHoa).HasMaxLength(256);
            entity.Property(e => e.TenDangNhap).HasMaxLength(256);
            entity.Property(e => e.TenDangNhapChuanHoa).HasMaxLength(256);

            entity.HasMany(d => d.VaiTros).WithMany(p => p.NguoiDungs)
                .UsingEntity<Dictionary<string, object>>(
                    "NguoiDungVaiTro",
                    r => r.HasOne<VaiTro>().WithMany()
                        .HasForeignKey("VaiTroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__NguoiDung__VaiTr__412EB0B6"),
                    l => l.HasOne<NguoiDung>().WithMany()
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__NguoiDung__Nguoi__403A8C7D"),
                    j =>
                    {
                        j.HasKey("NguoiDungId", "VaiTroId").HasName("PK__NguoiDun__B0CCFCAC9DC3BFDF");
                        j.ToTable("NguoiDungVaiTro");
                        j.IndexerProperty<string>("NguoiDungId")
                            .HasMaxLength(450)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("VaiTroId")
                            .HasMaxLength(450)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<PhuongXa>(entity =>
        {
            entity.HasKey(e => e.IdphuongXa).HasName("PK__PhuongXa__6D0023249441458D");

            entity.ToTable("PhuongXa");

            entity.Property(e => e.IdphuongXa)
                .ValueGeneratedNever()
                .HasColumnName("IDPhuongXa");
            entity.Property(e => e.IdquanHuyen).HasColumnName("IDQuanHuyen");
            entity.Property(e => e.TenPhuongXa).HasMaxLength(255);

            entity.HasOne(d => d.IdquanHuyenNavigation).WithMany(p => p.PhuongXas)
                .HasForeignKey(d => d.IdquanHuyen)
                .HasConstraintName("FK__PhuongXa__IDQuan__398D8EEE");
        });

        modelBuilder.Entity<QuanHuyen>(entity =>
        {
            entity.HasKey(e => e.IdquanHuyen).HasName("PK__QuanHuye__29AC36EE89A7DAB2");

            entity.ToTable("QuanHuyen");

            entity.Property(e => e.IdquanHuyen)
                .ValueGeneratedNever()
                .HasColumnName("IDQuanHuyen");
            entity.Property(e => e.TenQuanHuyen).HasMaxLength(255);
        });

        modelBuilder.Entity<ThongBaoThayDoi>(entity =>
        {
            entity.HasKey(e => e.IdthongBao).HasName("PK__ThongBao__3EBBFAAEA5DCC20F");

            entity.ToTable("ThongBaoThayDoi");

            entity.Property(e => e.IdthongBao).HasColumnName("IDThongBao");
            entity.Property(e => e.IdchuCoSoMoi)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasColumnName("IDChuCoSoMoi");
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");

            entity.HasOne(d => d.IdchuCoSoMoiNavigation).WithMany(p => p.ThongBaoThayDois)
                .HasForeignKey(d => d.IdchuCoSoMoi)
                .HasConstraintName("FK__ThongBaoT__IDChu__534D60F1");

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.ThongBaoThayDois)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__ThongBaoT__IDCoS__52593CB8");
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.IdtinTuc).HasName("PK__TinTuc__74C0F8F8112D2B25");

            entity.ToTable("TinTuc");

            entity.Property(e => e.IdtinTuc).HasColumnName("IDTinTuc");
            entity.Property(e => e.IdcanBo)
                .HasMaxLength(450)
                .IsUnicode(false)
                .HasColumnName("IDCanBo");
            entity.Property(e => e.Slug).HasMaxLength(160);
            entity.Property(e => e.TieuDe).HasMaxLength(160);

            entity.HasOne(d => d.IdcanBoNavigation).WithMany(p => p.TinTucs)
                .HasForeignKey(d => d.IdcanBo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TinTuc__IDCanBo__59063A47");

            entity.HasMany(d => d.IddanhMucs).WithMany(p => p.IdtinTucs)
                .UsingEntity<Dictionary<string, object>>(
                    "DanhMucBaiDang",
                    r => r.HasOne<DanhMuc>().WithMany()
                        .HasForeignKey("IddanhMuc")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DanhMucBa__IDDan__5FB337D6"),
                    l => l.HasOne<TinTuc>().WithMany()
                        .HasForeignKey("IdtinTuc")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DanhMucBa__IDTin__5EBF139D"),
                    j =>
                    {
                        j.HasKey("IdtinTuc", "IddanhMuc").HasName("PK__DanhMucB__493638450DD0A5B4");
                        j.ToTable("DanhMucBaiDang");
                        j.IndexerProperty<int>("IdtinTuc").HasColumnName("IDTinTuc");
                        j.IndexerProperty<int>("IddanhMuc").HasColumnName("IDDanhMuc");
                    });
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VaiTro__3214EC07CFE62517");

            entity.ToTable("VaiTro");

            entity.Property(e => e.Id)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.TenChuanHoa).HasMaxLength(256);
            entity.Property(e => e.TenVaiTro).HasMaxLength(256);
        });

        modelBuilder.Entity<XacThucNguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.NguoiDungId, e.NhaCungCapDangNhap, e.Ten }).HasName("PK__XacThucN__080C95996DE2325E");

            entity.ToTable("XacThucNguoiDung");

            entity.Property(e => e.NguoiDungId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.NhaCungCapDangNhap)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Ten)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.XacThucNguoiDungs)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__XacThucNg__Nguoi__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

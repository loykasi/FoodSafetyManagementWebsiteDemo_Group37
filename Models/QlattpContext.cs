using App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAnToanVeSinhThucPhamDemo.Models.Blog;
using WebAnToanVeSinhThucPhamDemo.Models.Contact;

namespace WebAnToanVeSinhThucPhamDemo.Models;

public partial class QlattpContext : IdentityDbContext<AppUser>
{
    public QlattpContext()
    {
        //WebAnToanVeSinhThucPhamDemo.Models.QlattpContext
    }

    public QlattpContext(DbContextOptions<QlattpContext> options)
        : base(options)
    {
    }
    public virtual DbSet<QuanHuyen> QuanHuyen { get; set; }

    public virtual DbSet<PhuongXa> PhuongXa { get; set; }
    public virtual DbSet<BanCongBoSp> BanCongBoSps { get; set; }

    public virtual DbSet<BaoCaoViPham> BaoCaoViPhams { get; set; }

    public virtual DbSet<ChiTietDoanThanhTra> ChiTietDoanThanhTras { get; set; }

    public virtual DbSet<ChiTietKetQua> ChiTietKetQuas { get; set; }

    public virtual DbSet<CoSo> CoSos { get; set; }

    public virtual DbSet<HoSoCapGiayChungNhan> HoSoCapGiayChungNhans { get; set; }

    public virtual DbSet<KeHoach> KeHoaches { get; set; }

    public virtual DbSet<KeHoachCoSo> KeHoachCoSos { get; set; }

    public virtual DbSet<MucKiemTra> MucKiemTras { get; set; }

    public virtual DbSet<ThongBaoThayDoi> ThongBaoThayDois { get; set; }

    public virtual DbSet<LienHe> LienHe { get; set; }

    public virtual DbSet<DanhMuc> DanhMuc { get; set; }

    public virtual DbSet<DanhMucBaiDang> DanhMucBaiDang { get; set; }
    public virtual DbSet<TinTuc> TinTuc { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasIndex(c => c.Slug)
                  .IsUnique();
        });

        modelBuilder.Entity<DanhMucBaiDang>(entity =>
        {
            entity.HasKey(c => new { c.IDBaiDang, c.IDDanhMuc });
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasIndex(p => p.Slug)
                  .IsUnique();
        });

        modelBuilder.Entity<BanCongBoSp>(entity =>
        {
            entity.HasKey(e => e.IdbanCongBoSp).HasName("PK__BanCongB__57824418B2AEB7B3");

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
                .HasConstraintName("FK__BanCongBo__IDCoS__440B1D61");
        });

        modelBuilder.Entity<BaoCaoViPham>(entity =>
        {
            entity.HasKey(e => e.IdbaoCao).HasName("PK__BaoCaoVi__BC216EF0A853FDD6");

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
                .HasConstraintName("FK__BaoCaoViP__IDCoS__4AB81AF0");
        });

        modelBuilder.Entity<ChiTietDoanThanhTra>(entity =>
        {
            entity.HasKey(e => new { e.IdkeHoach, e.IdCanBo }).HasName("PK__ChiTietD__4EE32992008214FC");
            entity.ToTable("ChiTietDoanThanhTra");

            entity.Property(e => e.IdkeHoach).HasColumnName("IDKeHoach");
            entity.Property(e => e.IdCanBo).HasColumnName("IDCanBo");

            entity.HasOne(d => d.CanBo)
                .WithMany()
                .HasForeignKey(d => d.CanBoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.IdkeHoachNavigation).WithMany(p => p.ChiTietDoanThanhTras)
                .HasForeignKey(d => d.IdkeHoach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__IDKeH__52593CB8");
        });
        modelBuilder.Entity<ChiTietKetQua>(entity =>
        {
            entity.HasKey(e => new { e.IdkeHoachCoSo, e.IdmucKt }).HasName("PK__ChiTietK__E62BAC593B114B18");

            entity.ToTable("ChiTietKetQua");

            entity.Property(e => e.IdkeHoachCoSo).HasColumnName("IDKeHoachCoSo");
            entity.Property(e => e.IdmucKt).HasColumnName("IDMucKT");

            entity.HasOne(d => d.IdkeHoachCoSoNavigation).WithMany(p => p.ChiTietKetQuas)
                .HasForeignKey(d => d.IdkeHoachCoSo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietKe__IDKeH__5BE2A6F2");

            entity.HasOne(d => d.IdmucKtNavigation).WithMany(p => p.ChiTietKetQuas)
                .HasForeignKey(d => d.IdmucKt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietKe__IDMuc__5CD6CB2B");
        });


        modelBuilder.Entity<CoSo>(entity =>
        {
            entity.HasKey(e => e.IdcoSo).HasName("PK__CoSo__344441C5716816E6");
            entity.ToTable("CoSo");
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.NgayCapCnattp).HasColumnName("NgayCapCNATTP");
            entity.Property(e => e.NgayCapGiayPhepKd).HasColumnName("NgayCapGiayPhepKD");
            entity.Property(e => e.NgayHetHanCnattp).HasColumnName("NgayHetHanCNATTP");
            entity.Property(e => e.SoGiayPhepKd)
                .IsUnicode(false)
                .HasColumnName("SoGiayPhepKD");

            // Thêm đoạn mã mới cho foreign key IDPhuongXa
            entity.HasOne(c => c.PhuongXa)
                .WithMany()
                .HasForeignKey(c => c.IDPhuongXa)
                .OnDelete(DeleteBehavior.Restrict);

            // Giữ nguyên đoạn mã cho foreign key IDChuCoSo
            entity.HasOne(c => c.ChuCoSo)
                .WithMany()
                .HasForeignKey(c => c.ChuCoSoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HoSoCapGiayChungNhan>(entity =>
        {
            entity.HasKey(e => e.IdgiayChungNhan).HasName("PK__HoSoCapG__729C7BB617160CD8");

            entity.ToTable("HoSoCapGiayChungNhan");

            entity.Property(e => e.IdgiayChungNhan).HasColumnName("IDGiayChungNhan");
            entity.Property(e => e.HinhAnhMinhChung).IsUnicode(false);
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.HoSoCapGiayChungNhans)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__HoSoCapGi__IDCoS__412EB0B6");
        });

        modelBuilder.Entity<KeHoach>(entity =>
        {
            entity.HasKey(e => e.IdkeHoach).HasName("PK__KeHoach__936F11C8A85196CC");

            entity.ToTable("KeHoach");

            entity.Property(e => e.IdkeHoach).HasColumnName("IDKeHoach");
            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
        });

        modelBuilder.Entity<KeHoachCoSo>(entity =>
        {
            entity.HasKey(e => e.IdkeHoachCoSo).HasName("PK__KeHoach___146E827EF1EC716F");

            entity.ToTable("KeHoach_CoSo");

            entity.Property(e => e.IdkeHoachCoSo).HasColumnName("IDKeHoachCoSo");
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");
            entity.Property(e => e.IdkeHoach).HasColumnName("IDKeHoach");
            entity.Property(e => e.ThoiGianKiemTra).HasColumnType("datetime");
            entity.Property(e => e.YkienChuCoSo).HasColumnName("YKienChuCoSo");

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.KeHoachCoSos)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__KeHoach_C__IDCoS__571DF1D5");

            entity.HasOne(d => d.IdkeHoachNavigation).WithMany(p => p.KeHoachCoSos)
                .HasForeignKey(d => d.IdkeHoach)
                .HasConstraintName("FK__KeHoach_C__IDKeH__5629CD9C");
        });

        modelBuilder.Entity<MucKiemTra>(entity =>
        {
            entity.HasKey(e => e.IdmucKt).HasName("PK__MucKiemT__2452E27ECB438FBA");

            entity.ToTable("MucKiemTra");

            entity.Property(e => e.IdmucKt).HasColumnName("IDMucKT");
        });

        modelBuilder.Entity<ThongBaoThayDoi>(entity =>
        {
            entity.HasKey(e => e.IdthongBao).HasName("PK__ThongBao__3EBBFAAEC5198DAB");
            entity.ToTable("ThongBaoThayDoi");

            entity.Property(e => e.IdthongBao).HasColumnName("IDThongBao");
            entity.Property(e => e.IdchuCoSoMoi).HasColumnName("IDChuCoSoMoi");
            entity.Property(e => e.IdcoSo).HasColumnName("IDCoSo");

            // Xóa đoạn mã cũ
            // entity.HasOne(d => d.IdchuCoSoMoiNavigation).WithMany(p => p.ThongBaoThayDois)
            //     .HasForeignKey(d => d.IdchuCoSoMoi)
            //     .HasConstraintName("FK__ThongBaoT__IDChu__47DBAE45");

            // Thêm đoạn mã mới
            entity.HasOne(t => t.ChuCoSoMoi)
                .WithMany()
                .HasForeignKey(t => t.ChuCoSoMoiId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.IdcoSoNavigation).WithMany(p => p.ThongBaoThayDois)
                .HasForeignKey(d => d.IdcoSo)
                .HasConstraintName("FK__ThongBaoT__IDCoS__46E78A0C");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

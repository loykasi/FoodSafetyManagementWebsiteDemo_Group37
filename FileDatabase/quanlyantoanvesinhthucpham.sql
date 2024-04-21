use master
go
drop database QLATTP
go
create database QLATTP
go
use QLATTP
go

create table ChucVu
(
	IDChucVu int primary key identity(1, 1),
	TenChucVu nvarchar(max)
)
go

create table CanBo
(
	IDCanBo int primary key identity(1, 1),
	HoTen nvarchar(max),
	CCCD varchar(12),
	MatKhau varchar(max),
	IDChucVu int,
	foreign key (IDChucVu) references ChucVu(IDChucVu)
)
go

create table ChuCoSo
(
	IDChuCoSo int primary key identity(1, 1),
	HoTen nvarchar(max),
	CCCD varchar(12),
	SDT varchar(11),
	MatKhau varchar(max)
)
go

create table CoSo
(
	IDCoSo int primary key identity(1, 1),
	IDChuCoSo int,
	TenCoSo nvarchar(max),
	DiaChi nvarchar(max),
	LoaiHinhKinhDoanh nvarchar(max),
	SoGiayPhepKD varchar(max),
	NgayCapGiayPhepKD date,
	NgayCapCNATTP date,
	NgayHetHanCNATTP date,
	foreign key (IDChuCoSo) references ChuCoSo(IDChuCoSo)
)
go

create table HoSoCapGiayChungNhan
(
	IDGiayChungNhan int primary key identity(1, 1),
	IDCoSo int,
	NgayDangKy date,
	LoaiThucPham nvarchar(max),
	HinhAnhMinhChung varchar(max),
	TrangThai int,
	foreign key (IDCoSo) references CoSo(IDCoSo)
)
go

create table BanCongBoSP
(
	IDBanCongBoSP int primary key identity(1, 1),
	IDCoSo int,
	NgayCongBo date,
	TenSanPham nvarchar(max),
	ThanhPhan nvarchar(max),
	ThoiHanSuDung date,
	CachDongGoi_BaoBi nvarchar(max),
	Ten_DiaChiSX nvarchar(max),
	MauNhanSP varchar(max),
	HinhAnhMinhChung varchar(max),
	TrangThai int,
	foreign key (IDCoSo) references CoSo(IDCoSo)
)
go

create table ThongBaoThayDoi
(
	IDThongBao int primary key identity(1, 1),
	IDCoSo int,
	IDChuCoSoMoi int,
	TenCoSoMoi nvarchar(max),
	DiaChiMoi nvarchar(max),
	TrangThai int,
	foreign key (IDCoSo) references CoSo(IDCoSo),
	foreign key (IDChuCoSoMoi) references ChuCoSo(IDChuCoSo)
)
go

create table BaoCaoViPham
(
	IDBaoCao bigint primary key identity(1, 1),
	HoTen nvarchar(max),
	SDT varchar(11),
	CCCD varchar(12),
	NgayBaoCao date,
	HinhAnhMinhChung varchar(max),
	IDCoSo int,
	foreign key (IDCoSo) references CoSo(IDCoSo)
)
go

create table TinTuc
(
	IDTinTuc int primary key identity(1, 1),
	TieuDe nvarchar(max),
	NoiDung text,
	NgayDang date,
	IDCanBo int,
	foreign key (IDCanBo) references CanBo(IDCanBo)
)
go

create table KeHoach
(
	IDKeHoach int primary key identity(1, 1),
	ThoiGianBatDau datetime,
	DoanSo int,
	Loai nvarchar(max)
)

create table ChiTietDoanThanhTra
(
	IDKeHoach int,
	IDCanBo int,
	ChucVu nvarchar(max),
	primary key(IDKeHoach, IDCanBo),
	foreign key (IDKeHoach) references KeHoach(IDKeHoach),
	foreign key (IDCanBo) references CanBo(IDCanBo)
)

create table KeHoach_CoSo
(
	IDKeHoachCoSo int primary key identity(1, 1),
	IDKeHoach int,
	IDCoSo int,
	ThoiGianKiemTra datetime,
	NgayTao date,
	KetLuan nvarchar(max),
	KhacPhuc nvarchar(max),
	YKienChuCoSo nvarchar(max),
	foreign key (IDKeHoach) references KeHoach(IDKeHoach),
	foreign key (IDCoSo) references CoSo(IDCoSo)
)

create table MucKiemTra
(
	IDMucKT int primary key identity(1, 1),
	NoiDung nvarchar(max),
)

create table ChiTietKetQua
(
	IDKeHoachCoSo int,
	IDMucKT int,
	Dat bit,
	primary key(IDKeHoachCoSo, IDMucKT),
	foreign key (IDKeHoachCoSo) references KeHoach_CoSo(IDKeHoachCoSo),
	foreign key (IDMucKT) references MucKiemTra(IDMucKT)
)


go

--tao procedure
-- insert hồ sơ với cơ sở mới 
create procedure insertGiayChungNhan_CoSo
	@IDChuCoSo int,
	@TenCoSo nvarchar(max),
	@DiaChi nvarchar(max),
	@LoaiHinhKinhDoanh nvarchar(max),
	@SoGiayPhepKD nvarchar(max),
	@NgayCapGiayPhepKD date,
	@LoaiThucPham nvarchar(max),
	@HinhAnhMinhChung varchar(max)
as
begin
	declare @NgayDangKy date = getdate()
	declare @TrangThai int = 0 --Trang thai -1: huybo	0:cho xet duyet	1:da duoc duyet
	insert into CoSo(IDChuCoSo,TenCoSo,DiaChi,LoaiHinhKinhDoanh,SoGiayPhepKD,NgayCapGiayPhepKD)
	values(@IDChuCoSo,@TenCoSo,@DiaChi,@LoaiHinhKinhDoanh,@SoGiayPhepKD,@NgayCapGiayPhepKD)
	declare @IDCoSo int
	set @IDCoSo = @@IDENTITY
	insert into HoSoCapGiayChungNhan(IDCoSo,NgayDangKy,LoaiThucPham,HinhAnhMinhChung,TrangThai)
	values (@IDCoSo,@NgayDangKy,@LoaiThucPham,@HinhAnhMinhChung,@TrangThai)
	declare @IDHoSo int
	set @IDHoSo = @@IDENTITY
	select @IDHoSo
end

go
exec insertGiayChungNhan_CoSo null,N'Co So A',N'48 Cao Thang',N'Nha Hang','KD123/456','3-20-2022',N'Com tam','hinhanh1.jpg'

--select * from CoSo
--select * from HoSoCapGiayChungNhan
go
-- insert hồ sơ với cơ sở đã có sẵn
create procedure insertGiayChungNhan
	@IDCoSo int,
	@LoaiThucPham nvarchar(max),
	@HinhAnhMinhChung varchar(max)
as
begin 
	declare @NgayDangKy date = getdate()
	declare @TrangThai int = 0 --Trang thai -1: huybo	0:cho xet duyet	1:da duoc duyet
	insert into HoSoCapGiayChungNhan(IDCoSo,NgayDangKy,LoaiThucPham,HinhAnhMinhChung,TrangThai)
	values (@IDCoSo,@NgayDangKy,@LoaiThucPham,@HinhAnhMinhChung,@TrangThai)
end
go
exec insertGiayChungNhan 1,N'Banh canh','hinhanh1.jpg'
exec insertGiayChungNhan_CoSo null,N'Co So A',N'48 Cao Thang',N'Nha Hang','KD123/456','3-20-2022',N'Com tam','hinhanh1.jpg'

--select * from CoSo
--select * from HoSoCapGiayChungNhan
go

--duyet ho so cap giay chung nhan
create procedure duyetGiayChungNhan
	@IDGiayChungNhan int
as
begin 
--0:chua duyet, 1:duyet, -1:huybo
	update HoSoCapGiayChungNhan
	set TrangThai= 1 
	where IDGiayChungNhan=@IDGiayChungNhan

	declare @IDCoSo int = ( select IDCoSo from HoSoCapGiayChungNhan where IDGiayChungNhan=@IDGiayChungNhan)
	declare @NgayXetDuyet date = getdate()
	declare @NgayHetHan date = dateadd(year,3,@NgayXetDuyet)
	update CoSo
	set NgayCapCNATTP=@NgayXetDuyet, NgayHetHanCNATTP=@NgayHetHan
	where IDCoSo = @IDCoSo
end
go
exec duyetGiayChungNhan 2
--select * from CoSo
--select * from HoSoCapGiayChungNhan
go
create procedure getGiayChungNhan
as
begin select * from HoSoCapGiayChungNhan 
end
go
--exec getGiayChungNhan

--select * from CoSo

create or alter proc pr_TaoBaoCaoViPham
	@HoTen nvarchar(max),
	@SDT varchar(11),
	@CCCD varchar(12),
	@NgayBaoCao date,
	@HinhAnhMinhChung varchar(max),
	@IDCoSo int,
	@NewId int output
as
begin
	insert into BaoCaoViPham (HoTen, SDT, CCCD, NgayBaoCao, HinhAnhMinhChung, IDCoSo)
	values (@HoTen, @SDT, @CCCD, @NgayBaoCao, @HinhAnhMinhChung, @IDCoSo)

	set @NewId = SCOPE_IDENTITY()
end
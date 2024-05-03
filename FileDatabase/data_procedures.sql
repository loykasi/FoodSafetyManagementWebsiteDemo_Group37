use QLATTP

SET IDENTITY_INSERT QuanHuyen ON
SET IDENTITY_INSERT PhuongXa OFF

insert into QuanHuyen(IDQuanHuyen,TenQuanHuyen)
values(1,N'Quận Cẩm Lệ'),
(2,N'Quận Hải Châu'),
(3,N'Quận Liên Chiểu'),
(4,N'Quận Ngũ Hành Sơn'),
(5,N'Quận Sơn Trà'),
(6,N'Quận Thanh Khê'),
(7,N'Huyện Hòa Vang'),
(8,N'Huyện Hoàng Sa')
go

SET IDENTITY_INSERT QuanHuyen OFF
SET IDENTITY_INSERT PhuongXa ON

insert into PhuongXa(IDPhuongXa,TenPhuongXa,IDQuanHuyen)
values(1,N'Phường Khuê Trung',1),
(2,N'Phường Hòa Phát',1),
(3,N'Phường Hòa An',1),
(4,N'Phường Hòa Thọ Tây',1),
(5,N'Phường Hòa Thọ Đông',1),
(6,N'Phường Hòa Xuân',1),
(7,N'Phường Thanh Bình',2),
(8,N'Phường Thuận Phước',2),
(9,N'Phường Thạch Thang',2),
(10,N'Phường Hải Châu I',2),
(11,N'Phường Hải Châu II',2),
(12,N'Phường Phước Ninh',2),
(13,N'Phường Hòa Thuận Tây',2),
(14,N'Phường Hòa Thuận Đông',2),
(15,N'Phường Nam Dương',2),
(16,N'Phường Bình Hiên',2),
(17,N'Phường Bình Thuận',2),
(18,N'Phường Hòa Cường Bắc',2),
(19,N'Phường Hòa Cường Nam',2)
go

SET IDENTITY_INSERT QuanHuyen OFF
SET IDENTITY_INSERT PhuongXa OFF

--tao procedure
-- insert hồ sơ với cơ sở mới 
go
create or alter procedure insertGiayChungNhan_CoSo
	@IDChuCoSo nvarchar(450),
	@TenCoSo nvarchar(max),
	@IDPhuongXa int,
	@DiaChi nvarchar(max),
	@LoaiHinhKinhDoanh nvarchar(max),
	@SoGiayPhepKD nvarchar(max),
	@NgayCapGiayPhepKD date,
	@LoaiThucPham nvarchar(max),
	@HinhAnhMinhChung varchar(max)
as
begin
	declare @NgayDangKy date = getdate()
	declare @TrangThai int = 0

	insert into CoSo(ChuCoSoId,TenCoSo,IDPhuongXa,DiaChi,LoaiHinhKinhDoanh,SoGiayPhepKD,NgayCapGiayPhepKD)
	values(@IDChuCoSo,@TenCoSo,@IDPhuongXa,@DiaChi,@LoaiHinhKinhDoanh,@SoGiayPhepKD,@NgayCapGiayPhepKD)
	declare @IDCoSo int = SCOPE_IDENTITY()

	insert into HoSoCapGiayChungNhan(IDCoSo,NgayDangKy,LoaiThucPham,HinhAnhMinhChung,TrangThai)
	values (@IDCoSo,@NgayDangKy,@LoaiThucPham,@HinhAnhMinhChung,@TrangThai)
	select SCOPE_IDENTITY()
end

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


create or alter proc pr_TaoBaoCaoViPham
	@HoTen nvarchar(max),
	@SDT varchar(11),
	@CCCD varchar(12),
	@NoiDung nvarchar(max),
	@NgayBaoCao date,
	@HinhAnhMinhChung varchar(max),
	@IDCoSo int,
	@NewId int output
as
begin
	insert into BaoCaoViPham (HoTen, SDT, CCCD, NoiDung, NgayBaoCao, HinhAnhMinhChung, IDCoSo)
	values (@HoTen, @SDT, @CCCD, @NoiDung, @NgayBaoCao, @HinhAnhMinhChung, @IDCoSo)

	set @NewId = SCOPE_IDENTITY()
end
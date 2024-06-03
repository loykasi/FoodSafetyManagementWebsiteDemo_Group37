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
(19,N'Phường Hòa Cường Nam',2),
(20,N'Phường Hòa Hiệp Bắc',3),
(21,N'Phường Hòa Hiệp Nam',3),
(22,N'Phường Hòa Khánh Bắc',3),
(23,N'Phường Hòa Khánh Nam',3),
(24,N'Phường Hòa Hòa Minh',3),
(25,N'Phường Hòa Hải',4),
(26,N'Phường Hòa Quý',4),
(27,N'Phường Khuê Mỹ',4),
(28,N'Phường Mỹ An',4),
(29,N'Phường An Hải Bắc',5),
(30,N'Phường An Hải Đông',5),
(31,N'Phường An Hải Tây',5),
(32,N'Phường Mân Thái',5),
(33,N'Phường Nại Hiên Đông',5),
(34,N'Phường Phước Mỹ',5),
(35,N'Phường Thọ Quang',5),
(36,N'Phường An Khê',6),
(37,N'Phường Chính Gián',6),
(38,N'Phường Hòa Khê',6),
(39,N'Phường Tam Thuận',6),
(40,N'Phường Tân Chính',6),
(41,N'Phường Thạc Gián',6),
(42,N'Phường Thạch Khê Đông',6),
(43,N'Phường Thạch Khê Tây',6),
(44,N'Phường Vĩnh Trung',6),
(45,N'Phường Xuân Hà',6),
(46,N'Xã Hòa Bắc',7),
(47,N'Xã Hòa Châu',7),
(48,N'Xã Hòa Khương',7),
(49,N'Xã Hòa Liên',7),
(50,N'Xã Hòa Nhơn',7),
(51,N'Xã Hòa Ninh',7),
(52,N'Xã Hòa Phong',7),
(53,N'Xã Hòa Phú',7),
(54,N'Xã Hòa Phước',7),
(55,N'Xã Hòa Sơn',7),
(56,N'Xã Hòa Tiến',7)
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
	select SCOPE_IDENTITY()
end

go
--duyet ho so cap giay chung nhan
CREATE OR ALTER PROCEDURE duyetGiayChungNhan
    @IDGiayChungNhan INT
AS
BEGIN 
    -- 0: chưa duyệt, 1: duyệt, -1: hủy bỏ
    UPDATE HoSoCapGiayChungNhan
    SET TrangThai = 1 
    WHERE IdgiayChungNhan = @IDGiayChungNhan;

    DECLARE @IDCoSo INT = (SELECT IdcoSo FROM HoSoCapGiayChungNhan WHERE IdgiayChungNhan = @IDGiayChungNhan);
    DECLARE @NgayXetDuyet DATE = GETDATE();
    DECLARE @NgayHetHan DATE = DATEADD(YEAR, 3, @NgayXetDuyet);

    UPDATE CoSo
    SET NgayCapCnattp = @NgayXetDuyet, NgayHetHanCnattp = @NgayHetHan
    WHERE IdcoSo = @IDCoSo;
END
GO
EXEC duyetGiayChungNhan @IDGiayChungNhan = 1;
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



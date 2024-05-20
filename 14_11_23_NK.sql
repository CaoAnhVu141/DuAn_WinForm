
---khởi tạo database 

create database DuAn2023_NK04


---tạo bảng cửa hàng

create table CuaHang
(
MaCH varchar(10),
TenCH nvarchar(50),
DiaChi nvarchar(50),
MaKho varchar(10),
MaNV varchar(10),
primary key(MaCH)
)



--tạo bảng nhân viên

create table NhanVien
(
MaNV varchar(10),
MaCH varchar(10),
TenNV nvarchar(50),
DiaChi nvarchar(50),
SDT varchar(10),
Luong varchar(20),
ChucVu nvarchar(20),
primary key (MaNV)
)





--tạo bảng Kho hàng

create table KhoHang
(
MaKho varchar(10),
TenKho nvarchar(50),
LSXuatHang date,
SoLuongTon varchar(70),
primary key (MaKho)
)


--tạo bảng sản phẩm

create table SanPham
(
MaSP varchar(10),
TenSP nvarchar(50),
NhaCungCap nvarchar(50),
Gia int,
MaCH varchar(10),
MoTa nvarchar(150)
primary key(MaSP)
)


--tạo bảng Danh sách sản phẩm


                                                                                                                                            
--tạo bảng Khách hàng

create table KhachHang
(
MaKH varchar(10),
TenKH nvarchar(50),
SDT varchar(10),
DiaChi nvarchar(50),
primary key(MaKH)
)







---Hoa don

create table HoaDon
(
MaHoaDon varchar(10),
MaDatHang varchar(10),
MaDonHang nvarchar(5),
MaSP varchar(10),
MaKH varchar(10),
primary key (MaHoaDon,MaDatHang,MaDonHang,MaSP,MaKH)
)





--tạo bảng mua sản phẩm

create table DatHang
(
  MaHieu INT IDENTITY(1,1) PRIMARY KEY,
  MaDatHang AS 'DH' + RIGHT('00' + CAST(MaHieu AS NVARCHAR(4)), 2),
  MaSP varchar(10),
  MaKH varchar(10),
  MaCH varchar(10),
  TenKH nvarchar(50),
  TenSanPham nvarchar(50),
  NhaCungCap nvarchar(50),
  DiaChi varchar(50),
  HinhThucMua nvarchar(50),
  NgayDatHang date,
  Gia int,

)



--tạo bảng đơn hàng

create table DonHang
(
MaDau INT IDENTITY(1,1),
MaDonHang AS 'DHA' + RIGHT('00' + CAST(MaDau AS NVARCHAR(4)), 2),
MaDatHang NVARCHAR(4),
MaSP varchar(10),
MaKH varchar(10),
TrangThaiDH nvarchar(50),
PTTT nvarchar(100),
NgayGHDK date,
GhiChu nvarchar(200),
PRIMARY KEY(MaDau,MaDatHang)
)




--nhận xét khách hàng



create table NhanXet
(
  MaSP varchar(10),
  MaKH varchar(10),
  NoiDung nvarchar(100),
  DiemDanhGia int
  primary key(MaSP,MaKH)

)

---tạo liên kết Cửa hàng và Nhân viên qua MaNV (1) 

alter table NhanVien
add constraint FK_NV_CH_MaCH
foreign key(MaCH)
references CuaHang(MaCH)




--tạo liên kết Cửa hàng và Kho hàng qua MaKho (2)

alter table CuaHang
add constraint FK_KH_CH_MaKho
foreign key(MaKho)
references KhoHang(MaKho)



--tạo liên kết giửa sản phẩm và cửa hàng qua MaCH (4)

alter table SanPham
add constraint FK_SP_CH_MaCH
foreign key(MaCH)
references CuaHang(MaCH)


---tạo liên kết sản phẩm và đơn hàng qua mã sản phẩm (5)

alter table DonHang
add constraint FK_SP_DonHang_MaSP
foreign key(MaSP)
references SanPham(MaSP)




--tạo liên kết khách hàng và đơn hàng qua mã khách hàng(5+)

alter table DonHang
add constraint FK_KH_DonHang_MaKH
foreign key(MaKH)
references KhachHang(MaKH)



--Tạo liên kết sản phẩm và đặt hàng qua ma sp (6) //

alter table DatHang
add constraint FK_SP_DatH_MaSP
foreign key(MaSP)
references SanPham(MaSP)


--tạo liên kết khách hàng và đặt hàng qua mã khách hàng (7) //

alter table DatHang
add constraint FK_KH_DatH_MaKH
foreign key(MaKH)
references KhachHang(MaKH)


--tạo liên kết cửa hàng và đặt hàng qua mã cửa hàng (7+)//

alter table DatHang
add constraint FK_CH_DatH_MaCH
foreign key(MaCH)
references CuaHang(MaCH)


--alter table DatHang
--drop constraint FK_CH_DatH_MaCH

--Tạo liên kết Nhận xét tới sản phẩm qua MaSP


alter table NhanXet
add constraint FK_NX_SP_MaSP
foreign key(MaSP)
references SanPham(MaSP)


--tạo liên kết bảng sản phẩm và hóa đơn qua mã sản phẩm(12) 

alter table HoaDon
add constraint FK_SP_HD_MaSP
foreign key(MaSP)
references SanPham(MaSP)




--tạo liên kết bảng khách hàng và hóa đơn qua mã khách hàng (13)

alter table HoaDon
add constraint FK_SP_HD_MaKH
foreign key(MaKH)
references KhachHang(MaKH)


--tạo liên kết đặt đặt hàng và hóa đơn qua mã đặt(14)//


--alter table HoaDon
--add constraint FK_DH_HD_MaDat
--foreign key(MaDatHang)
--references DatHang(MaDatHang)





--tạo liên kết đơn hàng và hóa đơn qua mã đơn hàng (15)

--alter table HoaDon
--add constraint FK_DH_HD_MaDH
--foreign key(MaDH)
--references DonHang(MaDH)



---xóa

------------------------------NẠP DỮ LIỆU CHO CÁC BẢNG-----------------------------------------


--Nạp dữ liệu cho bảng Nhân viên (1)


insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values ('NV01','CH01',N'Nguyễn Văn Nguyên',N'Hà Nội','012345678','120000',N'Nhân viên')
go

insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values ('NV02','CH02',N'Nguyễn Văn Hà',N'Hà Tĩnh','1123456728','210000',N'Quản lý')
go

insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values ('NV03','CH01',N'Trần Văn Bấc',N'Thủ Đức','012345678','100000',N'Bảo vệ')
go



insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values ('NV04','CH04',N'Nguyễn Thị Hân',N'Bắc Giang','111222234','120000',N'Nhân viên')
go

insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values ('NV05','CH06',N'Nguyễn Văn Nam',N'Nghệ An','012300078','120000',N'Nhân viên')
go

insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values ('NV06','CH05',N'Nguyễn Văn Nguyên',N'Cà Mau','022545678','160000',N'Phó quản lý')
go



--Nạp dữ liệu cho bảng Kho hàng (2)

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values ('KH01',N'Hà Nội 1','2022-1-23','210')
go

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values ('KH02',N'Hà Nội 2','2023-4-12','110')
go

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values ('KH03',N'Hà Nội 3','2021-6-23','160')
go

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values ('KH04',N'Hà Nội 4','2022-8-14','250')
go

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values ('KH05',N'Hà Nội 5','2022-12-11','300')
go

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values ('KH06',N'Hà Nội 6','2022-10-16','30')
go

-----Nạp dữ liệu cho bảng Cửa hàng (3)

insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values ('CH01',N'Cửa hàng 01',N'Hải Phòng','KH01','NV01')
go


insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values ('CH02',N'Cửa hàng 02',N'Hà Nội','KH02','NV03')
go

insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values ('CH03',N'Cửa hàng 04',N'TP HCM','KH03','NV03')
go

insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values ('CH04',N'Cửa hàng 04',N'Lạng Sơn','KH04','NV04')
go

insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values ('CH05',N'Cửa hàng 05',N'Đồng Nai','KH05','NV05')
go

insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values ('CH06',N'Cửa hàng 06',N'Bình Dương','KH06','NV06')
go




--nạp dữ liệu cho bảng đặt hàng

insert into DatHang(MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia)
values ('SP01','Cus01','CH01',N'Nguyễn Văn Bấc',N'Điều hòa',N'Xiaomi',N'Trung Thịnh',N'Tại cửa hàng','12/2/2022',340000)
go
insert into DatHang(MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia)
values ('SP02','Cus01','CH01',N'Nguyễn Văn Bấc',N'Điều hòa',N'Xiaomi',N'Trung Thịnh',N'Tại cửa hàng','12/2/2022',340000)
go

insert into DatHang(MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia)
values ('SP03','Cus02','CH01',N'Nguyễn Thị Vân',N'Điều hòa',N'Xiaomi',N'Ha Noi',N'Tại cửa hàng','12/2/2022',340000)
go






--------Nạp dữ liệu bảng Đơn hàng (5) --

insert into DonHang(MaDatHang,MaSP,MaKH,TrangThaiDH,PTTT,NgayGHDK,GhiChu)
values('DH07','SP06','Cus01',N'Hoàn tất',N'Tiền mặt','30/04/2023',N'Hàng dễ vỡ, chú ý')
go

insert into DonHang(MaDatHang,MaSP,MaKH,TrangThaiDH,PTTT,NgayGHDK,GhiChu)
values('DH14','SP03','Cus02',N'Hoàn tất',N'Tiền mặt','11/12/2023',N'Hàng dễ vỡ, chú ý')
go

insert into DonHang(MaDatHang,MaSP,MaKH,TrangThaiDH,PTTT,NgayGHDK,GhiChu)
values('DH15','SP04','Cus03',N'Hoàn tất',N'Chuyển khoản','11/8/2023',N'Hàng dễ vỡ, chú ý')
go

insert into DonHang(MaDatHang,MaSP,MaKH,TrangThaiDH,PTTT,NgayGHDK,GhiChu)
values('DH16','SP05','Cus04',N'Hoàn tất',N'Chuyển khoản','30/04/2023',N'Hàng đắt tiền')
go

insert into DonHang(MaDatHang,MaSP,MaKH,TrangThaiDH,PTTT,NgayGHDK,GhiChu)
values('DH17','SP02','Cus01',N'Hoàn tất',N'Chuyển khoản','12/4/2021',N'Hàng đắt tiền')
go

-----Nạp dữ liệu cho Khách hàng (6)

insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
Values ('Cus01',N'Nguyễn Văn Bấc','12345678',N'Thủ Đức')
go


insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
Values ('Cus02',N'Nguyễn Thị Hoa','123455633',N'Kiên Giang')
go

insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
Values ('Cus03',N'Trần Thái Thanh','0001111',N'Hà Nội')
go

insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
Values ('Cus04',N'Nguyễn Văn A, 1200022',N'Bắc Ninh')
go

insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
Values ('Cus05',N'Nguyễn Thị Thanh','12345645',N'Thủ Đức')
go

insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
Values ('Cus06',N'Cao Anh Vũ','12345678',N'TP HCM')
go




---Nạp dữ liệu cho danh sách sản phẩm (7)

insert into SanPham(MaSP,TenSP,NhaCungCap,Gia,MaCH,MoTa)
Values ('SP01',N'Điện thoại',N'Sam Sung','12000','CH01',N'Điện thoại nè')
go

insert into SanPham(MaSP,TenSP,NhaCungCap,Gia,MaCH,MoTa)
Values ('SP02',N'Máy tính',N'Dell','23000','CH02',N'Máy tính nè')
go

insert into SanPham(MaSP,TenSP,NhaCungCap,Gia,MaCH,MoTa)
Values ('SP03',N'Máy giặt',N'LG','100000','CH03',N'Máy giặt nè')
go

insert into SanPham(MaSP,TenSP,NhaCungCap,Gia,MaCH,MoTa)
Values ('SP04',N'Máy giặt',N'LG','100000','CH03',N'Máy giặt nè')
go


--insert into SanPham(MaSP,TenSP,NhaCungCap,Gia,MaCH,MaKH,MoTa)
--Values ('SP03',N'Máy giặt',N'LG','100000','CH03','Cus03',N'Máy giặt nè')
--go






--Nạp dữ liệu cho bảng Nhận xét (8)

insert into NhanXet(MaSP,MaKH,NoiDung,DiemDanhGia)
Values('SP01','Cus01',N'Ngon bổ rẻ nhan các bạn ơi',8)
go

insert into NhanXet(MaSP,MaKH,NoiDung,DiemDanhGia)
Values('SP02','Cus02',N'Ngon rẻ nhan các bạn ơi',9)
go

insert into NhanXet(MaSP,MaKH,NoiDung,DiemDanhGia)
Values('SP03','Cus03',N'Uy tín giá rẻ',7)
go


----Nạp dữ liệu cho hoa don (9) 


insert into HoaDon(MaHoaDon,MaDatHang,MaDonHang,MaSP,MaKH)
values('H02','DH14','DHA12','SP03','Cus02')
go





----------------------------VIẾT BẰNG STORE----------------------------

---Bảng xử lý nhân viên(1)

---Lấy danh sách Nhân viên
go
create proc sp_LaydanhsachNV
as 
select * from NhanVien 


---Thêm nhân viên 



go
create proc _spThemDSNV(@MaNV varchar(10),@MaCH varchar(10),@TenNV nvarchar(50),@Diachi nvarchar(50),@SDT varchar(10),@Luong varchar(20),@ChucVu nvarchar(20))
as
insert into NhanVien(MaNV,MaCH,TenNV,DiaChi,SDT,Luong,ChucVu)
Values(@MaNV,@MaCH,@TenNV,@Diachi,@SDT,@Luong,@ChucVu)



---Xóa nhân viên

go
create proc sp_XóaDSNV(@MaNV varchar(10))
as
begin

Delete  from NhanVien 
Where MaNV = @MaNV

end




---Sửa nhân viên


go
create proc _spSuaDSNV(@MaNV varchar(10),@MaCH varchar(10),@TenNV nvarchar(50),@Diachi nvarchar(50),@SDT varchar(10),@Luong varchar(20),@ChucVu nvarchar(20))
as

begin 

Update NhanVien 
Set TenNV = @TenNV,DiaChi = @Diachi,SDT = @SDT,Luong= @Luong,ChucVu = @ChucVu
where MaNV = @MaNV

end



---Lấy danh sách cửa hàng(2)
go
create proc sp_LayDSCH 

as 
select * from CuaHang
go


---Thêm danh sách cửa hàng

go
create proc sp_ThemDSCH(@MaCH varchar(10),@TenCH nvarchar(50),@Diachi nvarchar(50),@MaKho varchar(10),@MaNV varchar(10))
as
insert into CuaHang(MaCH,TenCH,DiaChi,MaKho,MaNV)
Values(@MaCH,@TenCH,@Diachi,@MaKho,@MaNV)


---Xóa danh sách cửa hàng


go
create proc sp_XoaDSCH(@MaCH varchar(10))
as

begin

delete from CuaHang
where MaCH = @MaCH

end

---Sửa cửa hàng

go
create proc sp_SuaDSCH(@MaCH varchar(10),@TenCH nvarchar(50),@Diachi nvarchar(50),@MaKho varchar(10),@MaNV varchar(10))
as
begin 

Update CuaHang
Set TenCH = @TenCH,DiaChi = @Diachi,MaKho= @MaKho, MaNV = @MaNV
where MaCH = @MaCH

end



---Lấy danh sách kho hàng(3)
go
create proc sp_LayDSCKH

as 
select * from KhoHang
go



---Thêm kho hàng

go
create proc sp_ThemDSKH(@Makho varchar(10),@TenKho nvarchar(50),@LSXuatHang date,@Soluongton varchar(70))

as

insert into KhoHang(MaKho,TenKho,LSXuatHang,SoLuongTon)
Values (@Makho,@TenKho,@LSXuatHang,@Soluongton)



------Xóa kho hàng
go
create proc sp_XoaDSKH(@Makho varchar(10))
as
begin
delete from KhoHang

where MaKho = @MaKho

end



---sửa kho hàng


go
create proc sp_SuaDSKH(@Makho varchar(10),@TenKho nvarchar(50),@LSXuatHang date,@Soluongton varchar(70))

as
begin 

Update KhoHang
Set TenKho = @TenKho,LSXuatHang = @LSXuatHang,SoLuongTon = @Soluongton
where MaKho = @Makho

end

-----lấy danh sách sản phẩm (4)

go
create proc sp_LayDSSP
as
select * from SanPham



---thêm sản phẩm



go
create proc spThemDSSP(@MaSP varchar(10),@TenSP nvarchar(50),@NhaCungCap nvarchar(50),@Gia int,@MaCH varchar(10),@MoTa nvarchar(150))

as 
insert into SanPham(MaSP,TenSP,NhaCungCap,Gia,MaCH,MoTa)
values(@MaSP,@TenSP,@NhaCungCap,@Gia,@MaCH,@MoTa)


---Xóa sản phẩm

go
create proc sp_XoaDSSP(@MaSP varchar(10))

as
begin

delete from SanPham
where MaSP = @MaSP

end



---sửa sản phẩm



go
create proc sp_SuaDSSP(@MaSP varchar(10),@TenSP nvarchar(50),@NhaCungCap nvarchar(50),@Gia int,@MaCH varchar(10),@MoTa nvarchar(150))

as 
begin

Update SanPham
set TenSP = @TenSP,NhaCungCap = @NhaCungCap,Gia = @Gia,MaCH = @MaCH,MoTa = @MoTa
where MaSP = @MaSP

end

---lấy danh sách khách hàng(5)

go 
create proc sp_LayDSKH

as 

select * from KhachHang

---thêm khách hàng

go
create proc sp_ThemDSKHH(@MaKHH varchar(10),@TenKHH nvarchar(50),@SDT varchar(10),@DiaChi nvarchar(50))

as
insert into KhachHang(MaKH,TenKH,SDT,DiaChi)
values(@MaKHH,@TenKHH,@SDT,@DiaChi)


---Xóa khách hàng

go
create proc sp_XoaDSKHH(@MaKH varchar(10))

as
begin

delete from KhachHang
where MaKH = @MaKH

end



---sửa sản phẩm

go
create proc sp_SuaDSKHH(@MaKHH varchar(10),@TenKHH nvarchar(50),@SDT varchar(10),@DiaChi nvarchar(50))

as 
begin

Update KhachHang
set TenKH = @TenKHH, SDT = @SDT,DiaChi = @DiaChi
where MaKH = @MaKHH

end



---lấy danh sách đặt hàng (6)

go
create proc sp_LayDatHang
as


select MaDatHang,MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia
from DatHang

---thêm danh sách đặt hàng

go
create proc sp_ThemDatHang(@MaSP varchar(10),@MaKH varchar(10),@MaCH varchar(10),@TenKH nvarchar(50),@TenSanPham nvarchar(50),@NhaCungCap nvarchar(50),@DiaChi varchar(50),@HinhThucMua nvarchar(50),@NgayDatHang date,@Gia int)
as

begin

insert into DatHang(MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia)
values(@MaSP,@MaKH,@MaCH,@TenKH,@TenSanPham,@NhaCungCap,@DiaChi,@HinhThucMua,@NgayDatHang,@Gia)

end


--xóa danh sách đặt hàng

go
create proc sp_Xoa(@MaDatHang varchar(10))

as

begin

delete from DatHang
where MaDatHang = @MaDatHang

end



--Sửa danh sách đặt hàng


go
create proc sp_SuaDatHang(@MaDatHang nvarchar(10),@MaSP varchar(10),@MaKH varchar(10),@MaCH varchar(10),@TenKH nvarchar(50),@TenSanPham nvarchar(50),@NhaCungCap nvarchar(50),@DiaChi nvarchar(50),@HinhThucMua nvarchar(50),@NgayDatHang date,@Gia int)

as

begin

Update DatHang

set TenKH = @TenKH,TenSanPham = @TenSanPham,NhaCungCap =@NhaCungCap,DiaChi = @DiaChi,HinhThucMua = @HinhThucMua,Gia = @Gia,
NgayDatHang = @NgayDatHang

where MaDatHang = @MaDatHang 

end





--lấy danh sách đơn hàng (7)


go
create proc sp_LayDSDHN

as


select DonHang.MaDonHang,DonHang.MaDatHang,DonHang.MaSP,DatHang.TenSanPham,DonHang.MaKH,DatHang.TenKH,DonHang.TrangThaiDH,DonHang.PTTT,DonHang.NgayGHDK,
DonHang.GhiChu,DatHang.Gia
from DonHang
INNER JOIN DatHang ON DatHang.MaDatHang = DonHang.MaDatHang




--thêm danh sách đặt hàng



go
create proc sp_ThemDonHang(@MaDatHang nvarchar(4),@MaSP varchar(10),@MaKH varchar(10),@TrangThai nvarchar(50),@PTTT nvarchar(100),@NgayGHDK date,@GhiChu nvarchar(200))
as
insert into DonHang(MaDatHang,MaSP,MaKH,TrangThaiDH,PTTT,NgayGHDK,GhiChu)
values(@MaDatHang,@MaSP,@MaKH,@TrangThai,@PTTT,@NgayGHDK,@GhiChu)
	
--

--insert into DonHang(MaDonHang,MaDatHang,MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia)
--select DatHang.*,MaDatHang,MaSP,MaKH,MaCH,TenKH,TenSanPham,NhaCungCap,DiaChi,HinhThucMua,NgayDatHang,Gia
--from DatHang
--INNER JOIN DonHang ON DatHang.MaDatHang = DonHang.MaDatHang


---xóa thông tin đơn hàng
go
create proc sp_XoaDonHang(@MaDonHang nvarchar(5),@MaDatHang nvarchar(4))

as

begin

delete from DonHang

where MaDonHang = @MaDonHang and MaDatHang = @MaDatHang

end

---sửa thông tin đơn hàng



go
create proc sp_SuaDonHang(@MaDatHang nvarchar(4),@MaSP varchar(10),@MaKH varchar(10),@TrangThai nvarchar(50),@PTTT nvarchar(100),@NgayGHDK date,@GhiChu nvarchar(200))

as

begin

Update DonHang

set TrangThaiDH = @TrangThai, PTTT = @PTTT,NgayGHDK = @NgayGHDK,GhiChu = @GhiChu
where MaDatHang = @MaDatHang 
end


----lấy danh sách nhận xét

go
create proc sp_LayDSNX

as
select * from NhanXet

---Thêm danh sách nhận xét

go
create proc sp_ThemDSNX(@MaSP varchar(10),@MaKH varchar(10), @NoiDung nvarchar(100),@DiemDanhGia int)

as
insert into NhanXet(MaSP,MaKH,NoiDung,DiemDanhGia)
values(@MaSP,@MaKH,@NoiDung,@DiemDanhGia)


---xóa danh sách đánh giá

go
create proc sp_XoaDSNX(@MaSP varchar(10),@MaKH varchar(10))
as
begin

delete from NhanXet
where MaSP = @MaSP and MaKH = @MaKH
end

---sửa danh sách lịch sử nhận xét

go
create proc sp_SuaDSNX(@MaSP varchar(10),@MaKH varchar(10), @NoiDung nvarchar(100),@DiemDanhGia int)

as

begin
Update NhanXet
set NoiDung = @NoiDung, DiemDanhGia = @DiemDanhGia

end

--
---Viết store tìm kiếm nhân viên

go
create proc sp_TKNhanVien(@MaNV varchar(10))
as
begin

select * from NhanVien
where MaNV = @MaNV

end

-----Viết store tìm kiếm cửa hàng

go
create proc sp_TK_CuaHang(@MaCH varchar(10))

as

begin

select * from CuaHang
where MaCH = @MaCH

end



-----Viết store tìm kiếm kho hàng

go
create proc sp_TK_KhoHang(@MaKho varchar(10))

as

begin

select * from KhoHang
where MaKho = @MaKho

end


-----Viết store tìm kiếm đơn hàng lỗi mã đơn hàng

go
create proc sp_TK_DonHang(@MaDonHang varchar(10))

as

begin

select * from DonHang
where MaDonHang = @MaDonHang

end



-----Viết store tìm kiếm khách hàng

go
create proc sp_TK_KhachHang(@MaKH varchar(10))

as

begin

select * from KhachHang
where MaKH = @MaKH

end


-----Viết store tìm kiếm sản phẩm

go
create proc sp_TK_SanPham(@MaSP varchar(10))

as

begin

select * from SanPham
where MaSP = @MaSP

end


--Viết store tìm kiếm đặt hàng

go
create proc sp_TK_DatHang(@MaDatHang nvarchar(4),@MaHieu int)
as
begin

select * from DatHang
where MaDatHang = @MaDatHang and MaHieu = @MaHieu

end



---viết store cho hóa đơn

--lấy danh sách hóa đơn


go
create proc sp_layHoaDon
as

SELECT HoaDon.MaHoaDon,DatHang.MaDatHang,DonHang.MaDonHang,SanPham.MaSP,SanPham.TenSP,KhachHang.MaKH,KhachHang.TenKH,KhachHang.DiaChi
,DatHang.NhaCungCap,DatHang.Gia,DonHang.PTTT
FROM HoaDon
LEFT JOIN DonHang ON HoaDon.MaDonHang = DonHang.MaDonHang
LEFT JOIN SanPham ON HoaDon.MaSP = SanPham.MaSP
LEFT JOIN KhachHang ON HoaDon.MaKH = KhachHang.MaKH 
LEFT JOIN DatHang ON HoaDon.MaDatHang = DatHang.MaDatHang


---tạo mới hóa đơn

go
create proc sp_TaoHoaDon(@MaHoaDon varchar(10),@MaDatHang varchar(10),@MaDonHang varchar(10),@MaSP varchar(10),@MaKH varchar(10))
as

insert into HoaDon(MaHoaDon,MaDatHang,MaDonHang,MaSP,MaKH)
values (@MaHoaDon,@MaDatHang,@MaDonHang,@MaSP,@MaKH)



---truy vấn test 




----select DatHang.MaDatHang,DonHang.MaSP,DonHang.MaKH,DonHang.MaCH,DonHang.TenKH,DonHang.TenSanPham,DonHang.NhaCungCap,
----DonHang.DiaChi,DonHang.HinhThucMua,DonHang.NgayDatHang,DonHang.Gia
----from DonHang
----INNER JOIN DatHang ON DatHang.MaDatHang = DonHang.MaDatHang
----where DonHang.MaDatHang = 'DH05'

----select HoaDon.MaHoaDon,DatHang.MaDatHang,KhachHang.MaKH,KhachHang.TenKH,SanPham.MaSP,SanPham.TenSP,
----DatHang.Gia,DonHang.NgayGHDK,DonHang.PTTT,DatHang.HinhThucMua
----from HoaDon
----INNER JOIN DonHang ON DonHang.MaDonHang = HoaDon.MaDonHang
----INNER JOIN DatHang ON DatHang.MaDatHang = DonHang.MaDatHang
----INNER JOIN SanPham ON SanPham.MaSP = DonHang.MaSP
----INNER JOIN KhachHang ON KhachHang.MaKH = DonHang.MaKH


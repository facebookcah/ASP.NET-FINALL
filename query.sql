create DATABASE FlowerShop
USE FlowerShop
GO
CREATE TABLE [dbo].[ChiTietGioHang](
	[MaGioHang] int,
	[MaSP] int NOT NULL,
	[SoLuongMua] int NOT NULL,
	[Gia] decimal(18, 0) NOT NULL,
	constraint pk primary key([MaGioHang],[MaSP])
)

CREATE TABLE [dbo].[DanhMuc](
	[MaDM] int IDENTITY(1,1) primary key,
	[TenDM] nvarchar(100) NOT NULL
)
CREATE TABLE [dbo].[GioHang](
	[MaGioHang] int IDENTITY(1,1) primary key,
	[TenTaiKhoan] varchar(100) NOT NULL
)
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] int IDENTITY(1,1) primary key,
	[NgayDat] datetime NOT NULL,
	[TinhTrang] nvarchar(100) NOT NULL,
	[PhiShip] money NOT NULL,
	[GhiChu] ntext NULL,
	[DcNhanHang] ntext NULL,
	[MaGioHang] int NOT NULL
)
CREATE TABLE [dbo].[SanPham](
	MaSP int IDENTITY(1,1) primary key,
	MaDM int NOT NULL,
	TenSP nvarchar(100) NOT NULL,
	HoaChinh nvarchar(100) NOT NULL,
	HoaPhu nvarchar(100) NOT NULL,
	ChieuNgang int not null,
	ChieuCao int not null,
	[TrongLuong] float,
	[SoLuongTon] int ,
	[Gia] decimal(18, 0) NOT NULL,
	[GiaKM] decimal(18, 0),
	[MoTa] ntext NULL,
	[Anh] ntext NOT NULL
)
CREATE TABLE [dbo].[TaiKhoan](
	[TenTaiKhoan] varchar(100) primary key,
	[MatKhau] varchar(100) NOT NULL,
	[Quyen] int NOT NULL,
	[TinhTrang] bit NOT NULL,
	[TenKhachHang] nvarchar(100) NULL,
	[Email] nvarchar(100) NULL,
	[SoDienThoai] varchar(12) NULL,
	[DiaChi] ntext NULL
)

ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD FOREIGN KEY([MaGioHang])
REFERENCES [dbo].[GioHang] ([MaGioHang])
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD FOREIGN KEY([TenTaiKhoan])
REFERENCES [dbo].[TaiKhoan] ([TenTaiKhoan])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaGioHang])
REFERENCES [dbo].[GioHang] ([MaGioHang])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([MaDM])
REFERENCES [dbo].[DanhMuc] ([MaDM])
GO

INSERT [dbo].[SanPham] (MaDM,TenSP,HoaChinh,HoaPhu,ChieuNgang,ChieuCao,[Gia],[GiaKM],[MoTa],[Anh]) VALUES
(1,N'Ấm áp',N' Hoa hồng Rednaomi, hoa hồng đỏ',N'Hoa cẩm chướng hồng, hoa hạnh phúc',40,50,1000000,800000,N'Màu đỏ chủ đạo là sự kết hợp dịu dàng và vô cùng ấm áp giữa hoa hồng đỏ và hoa cẩm chướng. Đây là cách bày tỏ tình yêu thương hiệu quả khi ta gửi gắm bình hoa này tới người nhận. Không cần nói lên lời, chỉ nhìn thấy hoa là người nhận đủ cảm thấy được tấm lòng của người tặng.','11.jpg')

,(1,N'Nhịp điệu yêu thương',N'Hoa hồng da',N'Hoa lan hồ điệp, hoa mõm sói',50,70,1000000,800000,N'Thời hiện đại 4.0 đã đẩy chúng ta vào vòng xoáy công việc, những ảnh hưởng bên ngoài mà quên đi cái nội tại bên trong. Đó là phải nuôi dưỡng tâm hồn bản thân, vun đắp mối quan hệ trong gia đình, người thân. Năng lượng tuyệt vời nhất để hun nóng tình cảm chính là những bông hoa tươi thắm, những màu hoa tươi sáng sẽ truyền tải cảm xúc và mang mọi người đến gần nhau hơn.','12.jpg')

,(1,N'Red roses',N'Hoa hồng da',N'Hoa lan hồ điệp, hoa mõm sói',50,70,1000000,800000,N'hahahahah','13.jpg')

,(1,N'Yêu vô bờ bến',N'10 hoa hồng đỏ RedNaomi, hoa hồng đỏ sasa',N' Hoa baby trắng, chuỗi ngọc',50,70,1000000,800000,N'Ai là fan cuồng của hoa hồng đỏ thì đây là món quà tuyệt vời để bạn tặng cho người thân. Là một trong những màu hoa được ưa chuộng của người Việt Nam, nên rất dễ để đi vào lòng người. Còn gì bằng khi bạn trao tặng món quà đáng yêu này cho người mà bạn đang hướng tới.','14.jpg')

,(1,N'Dịu dàng',N'Hoa hồng sen, hoa hồng da',N'Hoa lan',50,70,1000000,800000,N'Mảnh mai như hoa lan, tươi tắn như hoa hồng. Sự kết hợp dịu dàng và nhẹ nhàng này sẽ giúp bạn đem niềm vui, nụ cười và hạnh phúc đến với người mà bạn yêu thương','15.jpg')

,(1,N'Tình yêu mãi xanh',N'15 hoa hồng trắng',N'Hoa cẩm chướng xanh lá, hoa baby trắng',50,70,1000000,800000,N'Hoa hồng trắng tượng trưng cho tình yêu thanh thoát, tươi sáng. Xen lẫn vào là những điểm màu xanh của hoa cẩm chướng, tô thêm vị mát dịu và đầy hy vọng trong cuộc sống. Sự kết hợp tinh tế này giúp cho tình yêu giữa bạn và người bạn muốn tặng hoa trở nên bền chặt và tươi mới hơn.','16.jpg')

,(1,N'Big love',N'Hoa hồng đỏ',N'hoa hướng dương, hoa lan hồ điệp trắng',50,70,1000000,800000,N'Muôn cung bậc cảm xúc trong cuộc sống là minh chứng cho sự đa dạng màu sắc của các loài hoa. Sự tươi mới và tràn đầy hy vọng của hoa hướng dương, kết hợp với vẻ tinh khiết dịu dàng của hoa lan hồ điệp trắng, xen lẫn vào đó là điểm nhấn tình yêu nồng nàn của hoa hồng đỏ. Còn gì bằng khi đây là món quà quý để dành tặng những người thân trong gia đình với cầu mong sự an lành, no ấm và hanh phúc tràn đầy.','17.jpg')

,(1,N'Tươi trẻ',N' Hoa hồng Red Naomi, hoa hồng cam cà rốt',N'Hoa hướng dương, hoa lan, cúc ping pong, thiên điểu',50,70,1000000,800000,N'Tình yêu là món quà thượng đế trao tặng cho con người. Có được bản năng yêu và được yêu, thì cuộc sống giữa con người với con người trở nên đẹp đẽ và phong phú hơn. Để luôn phát triển mối quan hệ cao quý này, chúng ta cần tạo dây liên kết mật thiết và luôn nuôi dưỡng tình cảm bằng những giỏ hoa tươi xinh gửi đến đối phương. Đây là món quà tuyệt vời để dành tặng những dịp như sinh nhật, các ngày lễ... tạo cho cuộc sống luôn tươi mới và trẻ đẹp.','18.jpg')

,(1,N'Khát khao',N'Hoa hồng Red Naomi',N'Hoa hướng dương, hoa cúc ping pong, hoa thiên điểu',50,70,1000000,800000,N'Sự kết hợp giữa màu đỏ của hoa hồng và màu vàng của hướng dương lẫn hoa cúc giúp giỏ hoa đầy sức sống và khát vọng. Giữa cuộc sống với nhiều điều thú vị, chúng ta luôn khát khao tìm kiếm những cơ hội cho riêng mình. Vậy còn gì bằng cho sự hiện diện của chậu hoa này tới các dịp khai trương, kỷ niệm thành lập... giúp cho những người mà bạn yêu thương vươn tới những khát khao của họ và gặt hái được thành công.','19.jpg')

,(1,N'Love you',N'Hoa hồng da',N'Hoa cẩm chướng, hoa hồng môn',50,70,1000000,800000,N'Màu hồng nhẹ nhàng cùng kết hợp với màu tím thủy chung giúp các loài hoa này gửi đến thông điệp: Tình cảm của bạn và những người bạn yêu quý luôn phát triển tốt đẹp và bền vững. Sự hài hòa màu sắc của giỏ hoa giúp thu hút ánh nhìn của nhiều người, thích hợp tặng trong các dịp sinh nhật, gặp mặt, tri ân, kỉ niệm...','110.jpg')

INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa cao cấp')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa kỉ niệm')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa khai trương')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa sinh nhật')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa tình yêu')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa cưới')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Lẵng hoa')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa sự kiện')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa bó')
INSERT [dbo].[DanhMuc] ([TenDM]) VALUES ( N'Hoa ngày lễ')

INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (1, 2, 5, CAST(70000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (1, 3, 2, CAST(70000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (1, 4, 6, CAST(80000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (5, 10, 3, CAST(170000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (5, 9, 7, CAST(250000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (6, 8, 3, CAST(70000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (6, 7, 2, CAST(80000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (7, 2, 3, CAST(380000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (7, 6, 2, CAST(100000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSP], [SoLuongMua], [Gia]) VALUES (7, 9, 5, CAST(280000 AS Decimal(18, 0)))


INSERT [dbo].[GioHang] ([TenTaiKhoan]) VALUES ( N'phongnd')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'hopnt')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'phipv')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'anhntn')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'lamvv')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'hienlt')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'dunght')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'namnn')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'hoanghv')
INSERT [dbo].[GioHang] ( [TenTaiKhoan]) VALUES ( N'binhvv')


INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Đang giao', 15000.0000, N'', N'296/3 Cầu Diễn-MinhKhai-Hà nội', 1)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Chờ xác nhận', 15000.0000, N'', N'8 Hồ Tùng Mậu-Cầu Giấy-Hà Nội', 1)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Chờ xác nhận', 15000.0000, N'', N'20 Hồ Tùng Mậu-Cầu Giấy-Hà Nội', 1)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Đang giao', 15000.0000, N'', N'Ba Đình-Hà Nội', 1)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Đang giao', 15000.0000, N'', N'Trực Ninh - Nam Định', 2)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Chờ xác nhận', 15000.0000, N'', N'Trực Ninh - Nam Định',2)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Đang giao', 15000.0000, N'', N'Lê văn Luong-Thanh Xuân', 2)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Chờ xác nhận', 15000.0000, N'', N'Trực Ninh - Nam Định', 6)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:37:03.417' AS DateTime), N'Đã hủy', 15000.0000, N'', N'Ý Yên - Nam Định', 6)
INSERT [dbo].[HoaDon] ( [NgayDat], [TinhTrang], [PhiShip], [GhiChu], [DcNhanHang], [MaGioHang]) VALUES ( CAST(N'2021-08-08T15:39:41.000' AS DateTime), N'Đã hủy', 15000.0000, NULL, N'Đại học Công Nghiệp Hà Nội',7)





INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'phongnd', N'12345', 1, 1, N'phongnd2k1', N'phongnd@gmail.com', N'0378951234', N'Ý Yên-Nam Định')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'hopnt', N'12345', 0, 1, N'hopnt3456', N'hopnt@gmail.com', N'0983419342', N'Từ Sơn-Bắc Ninh')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'phipv', N'12345', 0, 1, N'phibd123', N'phipv@gmail.com', N'09999123421', N'Hải Hậu-Nam Định')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'anhntn', N'12345', 2, 1, N'Nguyễn Thị Ngọc Anh', N'anhntn@gmail.com', N'0378951234', N'Ba Vì-Hà Nội')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'lamvv', N'doan12345', 0, 1, N'Vũ Văn Lâm', N'lamvv@gmail.com', N'0987328540', N'Trực Ninh-Nam Định')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'hienlt', N'admin', 1, 0, N'Lê Thu Hiền', N'hienlt@gmail.com', N'0378951234', N'Hà Nội')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'dunght', N'dung123', 0, 1, N'Hoàng Trọng Dũng', N'dunght2k4@gmail.com', N'0955213512', N'Mê Linh - Hà Nội')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'namnn', N'12345', 0, 0, N'Nguyễn Ngọc Nam', N'namnguyen2k@gnamil.com', N'0992316236', N'Mê Linh - Hà Nội')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'hoanghv', N'12345', 0, 0, N'Hồ Văn Hoàng', N'hoanghv18@gnamil.com', N'0992316236', N'Mê Linh - Hà Nội')
INSERT [dbo].[TaiKhoan] ([TenTaiKhoan], [MatKhau], [Quyen], [TinhTrang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi]) VALUES (N'binhvv', N'12345', 0, 0, N'Vũ Văn Bình', N'binhvv231@gnamil.com', N'0992316236', N'Mê Linh - Hà Nội')



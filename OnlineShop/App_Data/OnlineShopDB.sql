CREATE DATABASE OnlineShopDB
USE OnlineShopDB

CREATE TABLE [dbo].[LoaiThanhVien] (
    [MaLoaiTV] INT           IDENTITY (1, 1) NOT NULL,
    [TenLoai]  NVARCHAR (50) NOT NULL,
    [UuDai]    FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([MaLoaiTV] ASC)
);

CREATE TABLE [dbo].[LoaiSanPham] (
    [MaLoaiSP] INT            IDENTITY (1, 1) NOT NULL,
    [TenLoai]  NVARCHAR (100) NOT NULL,
    [Icon]     NVARCHAR (MAX) NULL,
    [BiDanh]   NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([MaLoaiSP] ASC)
);

CREATE TABLE [dbo].[ThanhVien] (
    [MaTV]        INT            IDENTITY (1, 1) NOT NULL,
    [TaiKhoan]    NVARCHAR (100) NOT NULL,
    [MatKhau]     NVARCHAR (100) NOT NULL,
    [HoTen]       NVARCHAR (MAX) NOT NULL,
    [DiaChi]      NVARCHAR (MAX) NULL,
    [Email]       NVARCHAR (50)  NOT NULL,
    [SoDienThoai] VARCHAR (15)   NOT NULL,
    [CauHoi]      NVARCHAR (MAX) NULL,
    [CauTraLoi]   NVARCHAR (MAX) NULL,
    [MaLoaiTV]    INT            DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([MaTV] ASC),
    CONSTRAINT [FK_ThanhVien_ToTable] FOREIGN KEY ([MaLoaiTV]) REFERENCES [dbo].[LoaiThanhVien] ([MaLoaiTV]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[KhachHang] (
    [MaKH]        INT            IDENTITY (1, 1) NOT NULL,
    [TenKH]       NVARCHAR (100) NOT NULL,
    [DiaChi]      NVARCHAR (100) NOT NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [SoDienThoai] VARCHAR (15)   NOT NULL,
    [MaTV]        INT            NULL,
    PRIMARY KEY CLUSTERED ([MaKH] ASC),
    CONSTRAINT [FK_KhachHang_ToTable] FOREIGN KEY ([MaTV]) REFERENCES [dbo].[ThanhVien] ([MaTV]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[GioHang] (
    [MaGioHang] INT IDENTITY (1, 1) NOT NULL,
    [MaTV]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([MaGioHang] ASC),
    CONSTRAINT [FK_GioHang_ToTable] FOREIGN KEY ([MaTV]) REFERENCES [dbo].[ThanhVien] ([MaTV])
);

CREATE TABLE [dbo].[DonDatHang] (
    [MaDDH]             INT            IDENTITY (1, 1) NOT NULL,
    [NgayDat]           DATETIME       NOT NULL,
    [TinhTrangGiaoHang] BIT            DEFAULT ((0)) NOT NULL,
    [NgayGiao]          DATETIME       NULL,
    [DaThanhToan]       BIT            DEFAULT ((0)) NULL,
    [MaTV]              INT            NOT NULL,
    [UuDai]             INT            NULL,
    [HoVaTen]           NVARCHAR (MAX) NULL,
    [Email]             NVARCHAR (MAX) NULL,
    [SoDienThoai]       VARCHAR (15)   NULL,
    [Fax]               NVARCHAR (MAX) NULL,
    [CongTy]            NVARCHAR (MAX) NULL,
    [MaCongTy]          NVARCHAR (MAX) NULL,
    [DiaChiNhanHang]    NVARCHAR (MAX) NOT NULL,
    [DiaChiPhu]         NVARCHAR (MAX) NOT NULL,
    [LoiNhan]           NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([MaDDH] ASC),
    CONSTRAINT [FK_DonDatHang_ToTable] FOREIGN KEY ([MaTV]) REFERENCES [dbo].[ThanhVien] ([MaTV])
);

CREATE TABLE [dbo].[NhaSanXuat] (
    [MaNSX]    INT            IDENTITY (1, 1) NOT NULL,
    [TenNSX]   NVARCHAR (100) NOT NULL,
    [ThongTin] NVARCHAR (MAX) NOT NULL,
    [Logo]     NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([MaNSX] ASC)
);

CREATE TABLE [dbo].[NhaCungCap] (
    [MaNCC]       INT           IDENTITY (1, 1) NOT NULL,
    [TenNCC]      NVARCHAR (50) NOT NULL,
    [DiaChi]      NVARCHAR (50) NULL,
    [Email]       NVARCHAR (50) NULL,
    [SoDienThoai] VARCHAR (15)  NOT NULL,
    [Fax]         VARCHAR (30)  NULL,
    PRIMARY KEY CLUSTERED ([MaNCC] ASC)
);

CREATE TABLE [dbo].[SanPham] (
    [MaSP]         INT            IDENTITY (1, 1) NOT NULL,
    [TenSP]        NVARCHAR (MAX) NOT NULL,
    [DonGia]       FLOAT (53)     NOT NULL,
    [NgayCapNhat]  DATETIME       NOT NULL,
    [MoTa]         NVARCHAR (MAX) NOT NULL,
    [HinhAnh]      NVARCHAR (MAX) NULL,
    [SoLuongTon]   INT            NULL,
    [LuotXem]      INT            NULL,
    [LuotBinhChon] INT            NULL,
    [LuotBinhLuan] INT            NULL,
    [SoLanMua]     INT            NOT NULL,
    [Moi]          BIT            NULL,
    [MaNCC]        INT            NOT NULL,
    [MaNSX]        INT            NOT NULL,
    [MaLoaiSP]     INT            NOT NULL,
    [DaXoa]        BIT            DEFAULT ((0)) NOT NULL,
    [HinhAnhPhu1]  NVARCHAR (MAX) NULL,
    [HinhAnhPhu2]  NVARCHAR (MAX) NULL,
    [HinhAnhPhu3]  NVARCHAR (MAX) NULL,
    [HinhAnhPhu4]  NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([MaSP] ASC),
    CONSTRAINT [FK_SanPham_NhaCC] FOREIGN KEY ([MaNCC]) REFERENCES [dbo].[NhaCungCap] ([MaNCC]),
    CONSTRAINT [FK_SanPham_NhaSX] FOREIGN KEY ([MaNSX]) REFERENCES [dbo].[NhaSanXuat] ([MaNSX]),
    CONSTRAINT [FK_SanPham_LoaiSP] FOREIGN KEY ([MaLoaiSP]) REFERENCES [dbo].[LoaiSanPham] ([MaLoaiSP])
);

CREATE TABLE [dbo].[ChiTietDonDatHang] (
    [MaChiTietDDH] INT            IDENTITY (1, 1) NOT NULL,
    [MaDDH]        INT            NOT NULL,
    [MaSP]         INT            NOT NULL,
    [TenSP]        NVARCHAR (MAX) NOT NULL,
    [SoLuong]      INT            NOT NULL,
    [DonGia]       FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([MaChiTietDDH] ASC),
    CONSTRAINT [FK_ChiTietDonDatHang_ToTable] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP]),
    CONSTRAINT [FK_ChiTietDonDatHang_ToTable_1] FOREIGN KEY ([MaDDH]) REFERENCES [dbo].[DonDatHang] ([MaDDH])
);

CREATE TABLE [dbo].[SanPhamGioHang] (
    [MaSPGH]    INT            IDENTITY (1, 1) NOT NULL,
    [MaGioHang] INT            NOT NULL,
    [MaSP]      INT            NOT NULL,
    [TenSP]     NVARCHAR (MAX) NOT NULL,
    [SoLuong]   INT            NOT NULL,
    [DonGia]    FLOAT (53)     NOT NULL,
    [HinhAnh]   NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaSPGH] ASC),
    CONSTRAINT [FK_SanPhamGioHang_ToTable] FOREIGN KEY ([MaGioHang]) REFERENCES [dbo].[GioHang] ([MaGioHang])
);

CREATE TABLE [dbo].[PhieuNhap] (
    [MaPN]     INT      IDENTITY (1, 1) NOT NULL,
    [MaNCC]    INT      NOT NULL,
    [NgayNhap] DATETIME NOT NULL,
    [DaXoa]    BIT      DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([MaPN] ASC),
    CONSTRAINT [FK_PhieuNhap_ToTable] FOREIGN KEY ([MaNCC]) REFERENCES [dbo].[NhaCungCap] ([MaNCC]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[BinhLuan] (
    [MaBL]      INT            IDENTITY (1, 1) NOT NULL,
    [NoiDungBL] NVARCHAR (MAX) NOT NULL,
    [MaTV]      INT            NOT NULL,
    [MaSP]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([MaBL] ASC),
    CONSTRAINT [FK_BinhLuan_ToTable] FOREIGN KEY ([MaTV]) REFERENCES [dbo].[ThanhVien] ([MaTV]),
    CONSTRAINT [FK_BinhLuan_ToTable_1] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP])
);

CREATE TABLE [dbo].[ChiTietPhieuNhap] (
    [MaChiTietPN] INT        IDENTITY (1, 1) NOT NULL,
    [MaSP]        INT        NOT NULL,
    [MaPN]        INT        NOT NULL,
    [DonGiaNhap]  FLOAT (53) NOT NULL,
    [SoLuongNhap] FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaChiTietPN] ASC),
    CONSTRAINT [FK_ChiTietPhieuNhap_ToTable] FOREIGN KEY ([MaPN]) REFERENCES [dbo].[PhieuNhap] ([MaPN]),
    CONSTRAINT [FK_ChiTietPhieuNhap_ToTable_1] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP])
);

CREATE TABLE [dbo].[BoSuuTap] (
    [MaBST] INT        IDENTITY (1, 1) NOT NULL,
    [MaSP]        INT        NOT NULL,
    [MaTV]        INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([MaBST] ASC),
    CONSTRAINT [FK_BoSuuTap_ToTable] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP]),
    CONSTRAINT [FK_BoSuuTap_ToTable_1] FOREIGN KEY ([MaTV]) REFERENCES [dbo].[ThanhVien] ([MaTV])
);

CREATE TABLE [dbo].[MaGiamGia] (
    [Ma] INT        IDENTITY (1, 1) NOT NULL,
	[Ten] NVARCHAR(50) NOT NULL,
    [UuDai]        INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Ma] ASC),
);

CREATE TABLE [dbo].[MaCode] (
    [Ma] INT        IDENTITY (1, 1) NOT NULL,
	[Code] VARCHAR(7) NOT NULL,
    [UuDai]        INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Ma] ASC),
);

CREATE TABLE [dbo].[SanPhamDacBiet] (
    [Ma] INT        IDENTITY (1, 1) NOT NULL,
	[MaSP]        INT        NOT NULL,
	[GiamGia] int NOT NULL,
    [UuThich]        bit        NOT NULL,
    PRIMARY KEY CLUSTERED ([Ma] ASC),
	CONSTRAINT [FK_SanPhamDacBiet_ToTable] FOREIGN KEY ([MaSP]) REFERENCES [dbo].[SanPham] ([MaSP]),
);






















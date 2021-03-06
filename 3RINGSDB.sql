USE [master]
GO
/****** Object:  Database [3RINGSDB]    Script Date: 12/22/2020 14:21:06 ******/
CREATE DATABASE [3RINGSDB] ON  PRIMARY 
( NAME = N'3RINGSDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.PHUCGRANDE\MSSQL\DATA\3RINGSDB.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'3RINGSDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.PHUCGRANDE\MSSQL\DATA\3RINGSDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [3RINGSDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [3RINGSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [3RINGSDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [3RINGSDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [3RINGSDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [3RINGSDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [3RINGSDB] SET ARITHABORT OFF
GO
ALTER DATABASE [3RINGSDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [3RINGSDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [3RINGSDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [3RINGSDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [3RINGSDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [3RINGSDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [3RINGSDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [3RINGSDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [3RINGSDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [3RINGSDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [3RINGSDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [3RINGSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [3RINGSDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [3RINGSDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [3RINGSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [3RINGSDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [3RINGSDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [3RINGSDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [3RINGSDB] SET  READ_WRITE
GO
ALTER DATABASE [3RINGSDB] SET RECOVERY SIMPLE
GO
ALTER DATABASE [3RINGSDB] SET  MULTI_USER
GO
ALTER DATABASE [3RINGSDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [3RINGSDB] SET DB_CHAINING OFF
GO
USE [3RINGSDB]
GO
/****** Object:  Table [dbo].[UserRank]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRank](
	[RankID] [int] IDENTITY(1,1) NOT NULL,
	[RankName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RankID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](50) NULL,
	[Image] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[FullName] [nvarchar](100) NULL,
	[Price] [int] NULL,
	[Screen] [nvarchar](100) NULL,
	[ROM] [nvarchar](100) NULL,
	[RAM] [nvarchar](100) NULL,
	[FrontCamera] [nvarchar](500) NULL,
	[BehindCamera] [nvarchar](500) NULL,
	[Battery] [nvarchar](500) NULL,
	[BrandID] [int] NULL,
	[Quantity] [int] NULL,
	[ImportDate] [datetime] NULL,
	[Chipset] [nvarchar](100) NULL,
	[SIMCard] [nvarchar](100) NULL,
	[OS] [nvarchar](100) NULL,
	[Image] [nvarchar](500) NULL,
	[Image2] [nvarchar](500) NULL,
	[Image3] [nvarchar](500) NULL,
	[Image4] [nvarchar](500) NULL,
	[Image5] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[Design] [nvarchar](max) NULL,
	[Image6] [nvarchar](500) NULL,
	[Reason] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Address] [nvarchar](50) NULL,
	[Password] [nvarchar](20) NULL,
	[Point] [float] NULL,
	[RankID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Address] [nvarchar](50) NULL,
	[Password] [nvarchar](20) NULL,
	[RoleID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Content] [nvarchar](100) NULL,
	[RatingPoint] [float] NULL,
	[RatingDate] [datetime] NULL,
	[ProductID] [int] NULL,
	[RatingState] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RatingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Subject] [nvarchar](50) NULL,
	[Content] [nvarchar](500) NULL,
	[ContactDate] [datetime] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[ShippingAddress] [nvarchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[Price] [float] NULL,
	[StatusID] [int] NULL,
	[RecieveDate] [datetime] NULL,
	[Payment] [bit] NULL,
	[Note] [nvarchar](100) NULL,
	[ShippingCost] [float] NULL,
	[Discount] [float] NULL,
	[VAT] [float] NULL,
	[TotalPrice] [float] NULL,
	[CheckOut] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 12/22/2020 14:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[ReceiptID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ReceiptDate] [datetime] NULL,
	[TotalPrice] [float] NULL,
	[Payment] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BestSellers]    Script Date: 12/22/2020 14:21:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BestSellers]
AS
SELECT     TOP (100) PERCENT dbo.Product.ProductID, dbo.Product.ProductName, dbo.Product.Price, dbo.Product.Image, SUM(dbo.OrderDetail.Quantity) AS DaBan, 
                      dbo.Product.Price * SUM(dbo.OrderDetail.Quantity) AS TongTien
FROM         dbo.OrderDetail INNER JOIN
                      dbo.Product ON dbo.OrderDetail.ProductID = dbo.Product.ProductID INNER JOIN
                      dbo.Orders ON dbo.OrderDetail.OrderID = dbo.Orders.OrderID
WHERE     (dbo.Orders.StatusID = 4)
GROUP BY dbo.Product.ProductID, dbo.Product.ProductName, dbo.Product.Price, dbo.Product.Image
ORDER BY DaBan DESC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "OrderDetail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Orders"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 126
               Right = 404
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 442
               Bottom = 126
               Right = 602
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BestSellers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BestSellers'
GO
/****** Object:  ForeignKey [FK__Product__BrandID__164452B1]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([BrandID])
GO
/****** Object:  ForeignKey [FK__Customer__RankID__0DAF0CB0]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([RankID])
REFERENCES [dbo].[UserRank] ([RankID])
GO
/****** Object:  ForeignKey [FK__Admin__RoleID__0519C6AF]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
/****** Object:  ForeignKey [FK__Rating__ProductI__20C1E124]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
/****** Object:  ForeignKey [FK__Rating__UserID__1FCDBCEB]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Customer] ([UserID])
GO
/****** Object:  ForeignKey [FK__Contact__UserID__1B0907CE]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Customer] ([UserID])
GO
/****** Object:  ForeignKey [FK__Orders__StatusID__2A4B4B5E]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[OrderStatus] ([StatusID])
GO
/****** Object:  ForeignKey [FK__Orders__UserID__29572725]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Customer] ([UserID])
GO
/****** Object:  ForeignKey [FK__OrderDeta__Order__2F10007B]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
/****** Object:  ForeignKey [FK__OrderDeta__Produ__300424B4]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
/****** Object:  ForeignKey [FK__Receipt__OrderID__34C8D9D1]    Script Date: 12/22/2020 14:21:08 ******/
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO

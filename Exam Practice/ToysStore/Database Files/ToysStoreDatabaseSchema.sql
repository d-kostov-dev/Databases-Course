USE [master]
GO
/****** Object:  Database [ToysStore]    Script Date: 05-Sep-14 22:35:48 ******/
CREATE DATABASE [ToysStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToysStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.NEWSQL\MSSQL\DATA\ToysStore.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ToysStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.NEWSQL\MSSQL\DATA\ToysStore_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ToysStore] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToysStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToysStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToysStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToysStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToysStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToysStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToysStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToysStore] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ToysStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToysStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToysStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToysStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToysStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToysStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToysStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToysStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToysStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ToysStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToysStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToysStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToysStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToysStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToysStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToysStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToysStore] SET RECOVERY FULL 
GO
ALTER DATABASE [ToysStore] SET  MULTI_USER 
GO
ALTER DATABASE [ToysStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToysStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToysStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToysStore] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ToysStore', N'ON'
GO
USE [ToysStore]
GO
/****** Object:  Table [dbo].[AgeRanges]    Script Date: 05-Sep-14 22:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgeRanges](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MinAge] [int] NOT NULL,
	[MaxAge] [int] NOT NULL,
 CONSTRAINT [PK_AgeRanges] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 05-Sep-14 22:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 05-Sep-14 22:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Toys]    Script Date: 05-Sep-14 22:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Toys](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NULL,
	[ManufacturerId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Color] [nvarchar](20) NULL,
	[AgeRangeId] [int] NOT NULL,
 CONSTRAINT [PK_Toys] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ToysCategories]    Script Date: 05-Sep-14 22:35:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToysCategories](
	[ToyID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_ToysCategories] PRIMARY KEY CLUSTERED 
(
	[ToyID] ASC,
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Toys]  WITH CHECK ADD  CONSTRAINT [FK_Toys_AgeRanges] FOREIGN KEY([AgeRangeId])
REFERENCES [dbo].[AgeRanges] ([ID])
GO
ALTER TABLE [dbo].[Toys] CHECK CONSTRAINT [FK_Toys_AgeRanges]
GO
ALTER TABLE [dbo].[Toys]  WITH CHECK ADD  CONSTRAINT [FK_Toys_Manufacturers] FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Toys] CHECK CONSTRAINT [FK_Toys_Manufacturers]
GO
ALTER TABLE [dbo].[ToysCategories]  WITH CHECK ADD  CONSTRAINT [FK_ToysCategories_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([ID])
GO
ALTER TABLE [dbo].[ToysCategories] CHECK CONSTRAINT [FK_ToysCategories_Categories]
GO
ALTER TABLE [dbo].[ToysCategories]  WITH CHECK ADD  CONSTRAINT [FK_ToysCategories_Toys] FOREIGN KEY([ToyID])
REFERENCES [dbo].[Toys] ([ID])
GO
ALTER TABLE [dbo].[ToysCategories] CHECK CONSTRAINT [FK_ToysCategories_Toys]
GO
USE [master]
GO
ALTER DATABASE [ToysStore] SET  READ_WRITE 
GO

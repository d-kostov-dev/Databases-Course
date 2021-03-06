USE [master]
GO
/****** Object:  Database [CompanyEmployees]    Script Date: 9/8/2014 10:33:38 ******/
CREATE DATABASE [CompanyEmployees]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CompanyEmployees', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CompanyEmployees.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CompanyEmployees_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CompanyEmployees_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CompanyEmployees] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompanyEmployees].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompanyEmployees] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CompanyEmployees] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CompanyEmployees] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CompanyEmployees] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CompanyEmployees] SET ARITHABORT OFF 
GO
ALTER DATABASE [CompanyEmployees] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompanyEmployees] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyEmployees] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompanyEmployees] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompanyEmployees] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompanyEmployees] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CompanyEmployees] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CompanyEmployees] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompanyEmployees] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CompanyEmployees] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompanyEmployees] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CompanyEmployees] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompanyEmployees] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompanyEmployees] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompanyEmployees] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompanyEmployees] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompanyEmployees] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CompanyEmployees] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompanyEmployees] SET RECOVERY FULL 
GO
ALTER DATABASE [CompanyEmployees] SET  MULTI_USER 
GO
ALTER DATABASE [CompanyEmployees] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompanyEmployees] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompanyEmployees] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompanyEmployees] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [CompanyEmployees]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 9/8/2014 10:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Departments] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeProjects]    Script Date: 9/8/2014 10:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeProjects](
	[ProjectId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_EmployeeProjects] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 9/8/2014 10:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[ManagerId] [int] NULL,
	[Salary] [money] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 9/8/2014 10:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reports]    Script Date: 9/8/2014 10:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ReportDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EmployeeProjects]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjects_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[EmployeeProjects] CHECK CONSTRAINT [FK_EmployeeProjects_Employees]
GO
ALTER TABLE [dbo].[EmployeeProjects]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjects_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[EmployeeProjects] CHECK CONSTRAINT [FK_EmployeeProjects_Projects]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employees] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employees]
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD  CONSTRAINT [FK_Reports_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Reports] CHECK CONSTRAINT [FK_Reports_Employees]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [MinLengthConstraint] CHECK  ((datalength([Name])>(10)))
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [MinLengthConstraint]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [MinLengthConstraint_FirstName] CHECK  ((datalength([FirstName])>(5)))
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [MinLengthConstraint_FirstName]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [MinLengthConstraint_LastName] CHECK  ((datalength([LastName])>(5)))
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [MinLengthConstraint_LastName]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [MinLengthConstraint_Name] CHECK  ((datalength([Name])>(5)))
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [MinLengthConstraint_Name]
GO
USE [master]
GO
ALTER DATABASE [CompanyEmployees] SET  READ_WRITE 
GO

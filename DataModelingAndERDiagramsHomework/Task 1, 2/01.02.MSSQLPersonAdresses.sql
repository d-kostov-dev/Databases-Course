USE [PersonAdresses]
GO
/****** Object:  Table [dbo].[Adresses]    Script Date: 21-Aug-14 18:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adresses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AddressText] [nvarchar](255) NOT NULL,
	[TownID] [int] NOT NULL,
 CONSTRAINT [PK_Adresses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Continents]    Script Date: 21-Aug-14 18:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Continents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Continents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 21-Aug-14 18:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ContinentID] [int] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Persons]    Script Date: 21-Aug-14 18:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastNAme] [nvarchar](50) NOT NULL,
	[AddressID] [int] NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Towns]    Script Date: 21-Aug-14 18:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Towns](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryID] [int] NOT NULL,
 CONSTRAINT [PK_Towns] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Adresses] ON 

INSERT [dbo].[Adresses] ([id], [AddressText], [TownID]) VALUES (1, N'Mladost 1a', 1)
INSERT [dbo].[Adresses] ([id], [AddressText], [TownID]) VALUES (2, N'ul. Vinkel 1', 2)
SET IDENTITY_INSERT [dbo].[Adresses] OFF
SET IDENTITY_INSERT [dbo].[Continents] ON 

INSERT [dbo].[Continents] ([id], [Name]) VALUES (1, N'Europe')
INSERT [dbo].[Continents] ([id], [Name]) VALUES (2, N'Asia')
INSERT [dbo].[Continents] ([id], [Name]) VALUES (3, N'Africa')
SET IDENTITY_INSERT [dbo].[Continents] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([id], [Name], [ContinentID]) VALUES (1, N'Bulgaria', 1)
INSERT [dbo].[Countries] ([id], [Name], [ContinentID]) VALUES (2, N'Germany', 1)
INSERT [dbo].[Countries] ([id], [Name], [ContinentID]) VALUES (3, N'China', 2)
INSERT [dbo].[Countries] ([id], [Name], [ContinentID]) VALUES (4, N'Egypt', 3)
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([id], [FirstName], [LastNAme], [AddressID]) VALUES (1, N'Pesho', N'Goshov', 1)
INSERT [dbo].[Persons] ([id], [FirstName], [LastNAme], [AddressID]) VALUES (2, N'Bat', N'Joro', 2)
SET IDENTITY_INSERT [dbo].[Persons] OFF
SET IDENTITY_INSERT [dbo].[Towns] ON 

INSERT [dbo].[Towns] ([id], [Name], [CountryID]) VALUES (1, N'Sofia', 1)
INSERT [dbo].[Towns] ([id], [Name], [CountryID]) VALUES (2, N'Pernik', 1)
INSERT [dbo].[Towns] ([id], [Name], [CountryID]) VALUES (3, N'Munich', 2)
INSERT [dbo].[Towns] ([id], [Name], [CountryID]) VALUES (4, N'Beijing', 3)
INSERT [dbo].[Towns] ([id], [Name], [CountryID]) VALUES (5, N'Cairo', 4)
SET IDENTITY_INSERT [dbo].[Towns] OFF
ALTER TABLE [dbo].[Adresses]  WITH CHECK ADD  CONSTRAINT [FK_Adresses_Towns] FOREIGN KEY([TownID])
REFERENCES [dbo].[Towns] ([id])
GO
ALTER TABLE [dbo].[Adresses] CHECK CONSTRAINT [FK_Adresses_Towns]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Continents] FOREIGN KEY([ContinentID])
REFERENCES [dbo].[Continents] ([id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Continents]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Adresses] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Adresses] ([id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Adresses]
GO
ALTER TABLE [dbo].[Towns]  WITH CHECK ADD  CONSTRAINT [FK_Towns_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([id])
GO
ALTER TABLE [dbo].[Towns] CHECK CONSTRAINT [FK_Towns_Countries]
GO

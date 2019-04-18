USE [GuildCars1]
GO

ALTER TABLE [dbo].[PurchasedTable] DROP CONSTRAINT [FK__Purchased__BikeI__26CFC035]
GO

ALTER TABLE [dbo].[FeatureTable] DROP CONSTRAINT [FK__FeatureTa__BikeI__23F3538A]
GO

ALTER TABLE [dbo].[BikeTable] DROP CONSTRAINT [FK__BikeTable__BikeT__2116E6DF]
GO

ALTER TABLE [dbo].[BikeTable] DROP CONSTRAINT [FK__BikeTable__BikeM__1F2E9E6D]
GO

ALTER TABLE [dbo].[BikeTable] DROP CONSTRAINT [FK__BikeTable__BikeM__1E3A7A34]
GO

ALTER TABLE [dbo].[BikeTable] DROP CONSTRAINT [FK__BikeTable__BikeF__220B0B18]
GO

ALTER TABLE [dbo].[BikeTable] DROP CONSTRAINT [FK__BikeTable__BikeF__2022C2A6]
GO

ALTER TABLE [dbo].[BikeMakeTable] DROP CONSTRAINT [FK__BikeMakeT__BikeM__1975C517]
GO

ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

/****** Object:  Table [dbo].[SpecialTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[SpecialTable]
GO

/****** Object:  Table [dbo].[PurchasedTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[PurchasedTable]
GO

/****** Object:  Table [dbo].[FeatureTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[FeatureTable]
GO

/****** Object:  Table [dbo].[ContactTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[ContactTable]
GO

/****** Object:  Table [dbo].[ColorTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[ColorTable]
GO

/****** Object:  Table [dbo].[BikeTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[BikeTable]
GO

/****** Object:  Table [dbo].[BikeModelTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[BikeModelTable]
GO

/****** Object:  Table [dbo].[BikeMakeTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[BikeMakeTable]
GO

/****** Object:  Table [dbo].[BikeFrameTable]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[BikeFrameTable]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[AspNetUsers]
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[AspNetRoles]
GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/22/2019 5:24:41 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[firstName] [nvarchar](max) NULL,
	[lastName] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[BikeFrameTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BikeFrameTable](
	[BikeFrameId] [int] IDENTITY(1,1) NOT NULL,
	[BikeFrame] [char](64) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BikeFrameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[BikeMakeTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BikeMakeTable](
	[BikeMakeId] [int] IDENTITY(1,1) NOT NULL,
	[BikeModelId] [int] NULL,
	[BikeMake] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BikeMakeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[BikeModelTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BikeModelTable](
	[BikeModelId] [int] IDENTITY(1,1) NOT NULL,
	[BikeModel] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BikeModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[BikeTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BikeTable](
	[BikeId] [int] IDENTITY(1,1) NOT NULL,
	[BikeMakeId] [int] NOT NULL,
	[BikeModelId] [int] NOT NULL,
	[BikeFrameColorId] [int] NOT NULL,
	[BikeTrimColorId] [int] NOT NULL,
	[BikeFrameId] [int] NOT NULL,
	[BikeMsrp] [decimal](18, 0) NOT NULL,
	[BikeListPrice] [decimal](18, 0) NOT NULL,
	[BikeYear] [int] NOT NULL,
	[BikeIsNew] [binary](1) NOT NULL,
	[BikeCondition] [int] NOT NULL,
	[BikeNumGears] [int] NOT NULL,
	[BikeSerialNum] [char](20) NOT NULL,
	[BikeDescription] [text] NOT NULL,
	[BikeDateAdded] [date] NOT NULL,
	[BikePictName] [char](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[BikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ColorTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ColorTable](
	[BikeColorId] [int] IDENTITY(1,1) NOT NULL,
	[BikeColor] [char](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BikeColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ContactTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContactTable](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[CntctLastName] [varchar](64) NOT NULL,
	[CntctFirstName] [varchar](32) NOT NULL,
	[CntctPhone] [char](12) NULL,
	[CntctEmail] [varchar](32) NULL,
	[CntctMessage] [varchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FeatureTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FeatureTable](
	[FeatureId] [int] NOT NULL,
	[BikeId] [int] NULL,
	[FeatureDescription] [varchar](256) NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PurchasedTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PurchasedTable](
	[PurchaseSaleId] [int] IDENTITY(1,1) NOT NULL,
	[BikeId] [int] NOT NULL,
	[PurchasedPrice] [decimal](18, 0) NOT NULL,
	[PurchCustFirst] [varchar](32) NULL,
	[PurchCustLast] [varchar](64) NULL,
	[PurchCustPhone] [varchar](16) NULL,
	[PurchCustAddress1] [varchar](64) NULL,
	[PurchCustAddress2] [varchar](64) NULL,
	[PurchCustCity] [varchar](64) NULL,
	[PurchCustState] [varchar](16) NULL,
	[PurchCustPostCode] [varchar](16) NULL,
	[PurchFinType] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseSaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SpecialTable]    Script Date: 3/22/2019 5:24:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpecialTable](
	[SpecialId] [int] IDENTITY(1,1) NOT NULL,
	[SpecialTitle] [varchar](48) NOT NULL,
	[Description] [varchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SpecialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[BikeMakeTable]  WITH CHECK ADD FOREIGN KEY([BikeModelId])
REFERENCES [dbo].[BikeModelTable] ([BikeModelId])
GO

ALTER TABLE [dbo].[BikeTable]  WITH CHECK ADD FOREIGN KEY([BikeFrameColorId])
REFERENCES [dbo].[ColorTable] ([BikeColorId])
GO

ALTER TABLE [dbo].[BikeTable]  WITH CHECK ADD FOREIGN KEY([BikeFrameId])
REFERENCES [dbo].[BikeFrameTable] ([BikeFrameId])
GO

ALTER TABLE [dbo].[BikeTable]  WITH CHECK ADD FOREIGN KEY([BikeMakeId])
REFERENCES [dbo].[BikeMakeTable] ([BikeMakeId])
GO

ALTER TABLE [dbo].[BikeTable]  WITH CHECK ADD FOREIGN KEY([BikeModelId])
REFERENCES [dbo].[BikeModelTable] ([BikeModelId])
GO

ALTER TABLE [dbo].[BikeTable]  WITH CHECK ADD FOREIGN KEY([BikeTrimColorId])
REFERENCES [dbo].[ColorTable] ([BikeColorId])
GO

ALTER TABLE [dbo].[FeatureTable]  WITH CHECK ADD FOREIGN KEY([BikeId])
REFERENCES [dbo].[BikeTable] ([BikeId])
GO

ALTER TABLE [dbo].[PurchasedTable]  WITH CHECK ADD FOREIGN KEY([BikeId])
REFERENCES [dbo].[BikeTable] ([BikeId])
GO




-- -  -   -    -     -      -       -        -
-- Note: We can't start in the normal fasion of looking for
-- and removing the db bcause Entity adds crtical info to the db
-- Instead, we remove the non-Entity tables.

USE [GuildCars1]
GO

-- -  -   -    -     -      -       -        -
-- Remove Tables

IF EXISTS(SELECT * FROM sys.tables WHERE name='CustomerTable')
	DROP TABLE CustomerTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='SpecialTable')
	DROP TABLE SpecialTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='RoleTable')
	DROP TABLE RoleTable
GO
-- -  -   -    -     -      -       -        -
-- The tables avove have no FKs and are not used as FKs in other tables

IF EXISTS(SELECT * FROM sys.tables WHERE name='FeatureTable')
	DROP TABLE FeatureTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='SaleTable')
	DROP TABLE SaleTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeTable')
	DROP TABLE BikeTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='FrameTable')
	DROP TABLE FrameTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ColorTable')
	DROP TABLE ColorTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='MakeTable')
	DROP TABLE MakeTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ModelTable')
	DROP TABLE ModelTable
GO

-- -  -   -    -     -      -       -        -

CREATE TABLE FrameTable ( 
	FrameId              int primary key identity(1,1) not null,
	Frame                char(64) NOT NULL,
 ) 
GO

CREATE TABLE ColorTable ( 
	ColorId              int primary key identity(1,1) not null,
	Color                char(32) NOT NULL,
 )
GO 

CREATE TABLE CustomerTable ( 
	CustId               int primary key identity(1,1) not null,
	LastName             varchar(64)  NOT NULL,
	FirstName            varchar(32)  NOT NULL,
	Email                varchar(32)  NOT NULL
 ) 
GO

CREATE TABLE ModelTable ( 
	ModelId              int primary key identity(1,1) not null,
	Model                varchar(32)  NOT NULL,
 ) 
GO

CREATE TABLE MakeTable ( 
	MakeId               int primary key identity(1,1) not null,
	ModelId int foreign key references ModelTable(ModelId) null,	
	Make                 varchar(32)  NOT NULL,
)
GO

CREATE TABLE RoleTable ( 
	RoleId               int primary key identity(1,1) not null,
	Role                 varchar(64) NOT NULL,
 ) 

CREATE TABLE SpecialTable ( 
	SpecialId            int primary key identity(1,1) not null,
	Title                varchar(48)  NOT NULL,
	Description          varchar(256) NOT NULL,
 ) 

CREATE TABLE BikeTable ( 
	BikeId            int primary key identity(1,1) NOT NULL,
	MakeId int foreign key references MakeTable(MakeId) NOT NULL,
	ModelId int foreign key references ModelTable(ModelId) NOT NULL,	
	FrameColorId int foreign key references ColorTable(ColorId) NOT NULL,	
	TrimColorId int foreign key references ColorTable(ColorId) NOT NULL,
	FrameId int foreign key references FrameTable(FrameId) NOT NULL,
		
	Msrp                 int  NOT NULL,
	[year]               int  NOT NULL,
	IsNew                binary(1) NOT NULL,
	IsAutom              binary(1) NOT NULL,
	Miles                int NOT NULL, 
	Vin                  char(20)  NOT NULL,
	Description          text  NOT NULL,
	DateAdded            date  NOT NULL,
	PictName             char(64)
 ) 
GO

CREATE TABLE FeatureTable ( 
	FeatureId            int  NOT NULL,
	BikeId int foreign key references BikeTable(BikeId) null,	
	Description          varchar(256),
 ) 
GO

CREATE TABLE SaleTable ( 
	SaleId               int primary key identity(1,1) NOT NULL,
	BikeId int foreign key references BikeTable(BikeId) NOT NULL,
	Price                decimal  NOT NULL,
	PurchType            varchar(20)  NOT NULL,
 ) 


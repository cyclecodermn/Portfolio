
-- -  -   -    -     -      -       -        -
-- Note: We can't start in the normal fasion of looking for
-- and removing the db bcause Entity adds crtical info to the db
-- Instead, we remove the non-Entity tables.

USE [GuildCars1]
GO

-- -  -   -    -     -      -       -        -
-- Remove Tables

IF EXISTS(SELECT * FROM sys.tables WHERE name='ContactTable')
	DROP TABLE ContactTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='SpecialTable')
	DROP TABLE SpecialTable
GO

-- -  -   -    -     -      -       -        -
-- The tables avove have no FKs and are not used as FKs in other tables

IF EXISTS(SELECT * FROM sys.tables WHERE name='FeatureTable')
	DROP TABLE FeatureTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchasedTable')
	DROP TABLE PurchasedTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeTable')
	DROP TABLE BikeTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeFrameTable')
	DROP TABLE BikeFrameTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ColorTable')
	DROP TABLE ColorTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeMakeTable')
	DROP TABLE BikeMakeTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeModelTable')
	DROP TABLE BikeModelTable
GO

-- -  -   -    -     -      -       -        -

CREATE TABLE BikeFrameTable ( 
	BikeFrameId              int primary key identity(1,1) not null,
	BikeFrame                varchar(64) NULL,
 ) 
GO

CREATE TABLE ColorTable ( 
	BikeColorId              int primary key identity(1,1) not null,
	BikeColor                char(32) NULL,
 )
GO 

CREATE TABLE ContactTable ( 
	ContactId               int primary key identity(1,1) not null,
	CntctLastName             varchar(64)  NULL,
	CntctFirstName            varchar(32)  NULL,
	CntctPhone				 char(12),
	CntctEmail                varchar(32),
	CntctMessage				 varchar(256)
 ) 
GO

CREATE TABLE BikeModelTable ( 
	BikeModelId              int primary key identity(1,1) not null,
	BikeModel                varchar(32)  NULL,
	ModelAddedDate datetime NULL DEFAULT GETDATE() 
 ) 
GO

CREATE TABLE BikeMakeTable ( 
	BikeMakeId               int primary key identity(1,1) not null,
	BikeModelId int foreign key references BikeModelTable(BikeModelId) null,	
	BikeMake                 varchar(32)  NULL,
	MakeAddedDate datetime NULL DEFAULT GETDATE() 
)
GO

CREATE TABLE SpecialTable ( 
	SpecialId            int primary key identity(1,1) not null,
	SpecialTitle                varchar(48)  NULL,
	SpecialDescription          varchar(256) NULL,
 ) 

CREATE TABLE BikeTable ( 
	BikeId            int primary key identity(1,1) NOT NULL,
	BikeMakeId int foreign key references BikeMakeTable(BikeMakeId) NULL,
	BikeModelId int foreign key references BikeModelTable(BikeModelId)  NULL,	
	BikeFrameColorId int foreign key references ColorTable(BikeColorId) NULL,	
	BikeTrimColorId int foreign key references ColorTable(BikeColorId) NULL,
	BikeFrameId int foreign key references BikeFrameTable(BikeFrameId) NULL,
		
	BikeMsrp                 dec  NULL,
	BikeListPrice            dec  NULL,
	BikeYear               int  NULL,
	BikeIsNew                binary(1) NULL,
	BikeCondition			 int NULL,
	BikeNumGears             int NULL,
	BikeSerialNum            char(20)  NULL,
	BikeDescription          text  NULL,
	BikeDateAdded            date  NULL,
	BikePictName             char(64)
 ) 
GO

CREATE TABLE FeatureTable ( 
	FeatureId            int primary key identity(1,1) NOT NULL,
	BikeId int foreign key references BikeTable(BikeId) null,	
	FeatureDescription          varchar(256),
 ) 
GO

CREATE TABLE PurchasedTable ( 
	PurchaseSaleId               int primary key identity(1,1) NOT NULL,
	BikeId int foreign key references BikeTable(BikeId) NULL,
	PurchasedPrice       decimal  NULL,
	PurchCustFirst			varchar(32),
	PurchCustLast			varchar(64),
	PurchCustPhone			varchar(16),
	PurchCustAddress1		varchar(64),
	PurchCustAddress2		varchar(64),
	PurchCustCity			varchar(64),
	PurchCustState			varchar(16),
	PurchCustPostCode		varchar(16),
	PurchFinType           varchar(32)  NULL
 ) 


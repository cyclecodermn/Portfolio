
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

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeColorTable')
	DROP TABLE BikeColorTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeMakeTable')
	DROP TABLE BikeMakeTable
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BikeModelTable')
	DROP TABLE BikeModelTable
GO

-- -  -   -    -     -      -       -        -

CREATE TABLE BikeFrameTable ( 
	BikeFrameId		int primary key identity(1,1) not null,
	BikeFrameName	varchar(64) NOT NULL,
 ) 
GO

CREATE TABLE BikeColorTable ( 
	BikeColorId		int primary key identity(1,1) not null,
	BikeColor		varchar(32) NOT NULL,
 )
GO 

CREATE TABLE ContactTable ( 
	ContactId		int primary key identity(1,1) not null,
	CntctLastName	varchar(64)  NOT NULL,
	CntctFirstName	varchar(32)  NOT NULL,
	CntctPhone		varchar(15),
	CntctEmail		varchar(32),
	CntctMessage	varchar(256)
 ) 
GO

CREATE TABLE BikeMakeTable ( 
	BikeMakeId		int primary key identity(1,1) not null,
	BikeMake		varchar(32)  NOT NULL,
	MakeAddedDate	datetime NOT NULL DEFAULT GETDATE() 
)
GO
CREATE TABLE BikeModelTable ( 
	BikeModelId		int primary key identity(1,1) not null,
	BikeMakelId		int foreign key references BikeModelTable(BikeModelId) null,	
	BikeModel		varchar(32)  NOT NULL,
	ModelAddedDate	datetime NOT NULL DEFAULT GETDATE() 
 ) 
GO

CREATE TABLE SpecialTable ( 
	SpecialId			int primary key identity(1,1) not null,
	SpecialTitle		varchar(48)  NOT NULL,
	SpecialDescription	varchar(256) NOT NULL,
 ) 

CREATE TABLE BikeTable ( 
	BikeId				int primary key identity(1,1) NOT NULL,
	BikeMakeId			int foreign key references BikeMakeTable(BikeMakeId) NOT NULL,
	BikeModelId			int foreign key references BikeModelTable(BikeModelId) NOT NULL,	
	BikeFrameColorId	int foreign key references BikeColorTable(BikeColorId) NOT NULL,	
	BikeTrimColorId		int foreign key references BikeColorTable(BikeColorId) NOT NULL,
	BikeFrameId			int foreign key references BikeFrameTable(BikeFrameId) NOT NULL,
		
	BikeMsrp			money  NOT NULL,
	BikeListPrice		money  NOT NULL,
	BikeYear			int  NOT NULL,
	BikeIsNew			bit NOT NULL,
	BikeCondition		int NOT NULL,
	BikeNumGears		int NOT NULL,
	BikeSerialNum		varchar(20)  NOT NULL,
	BikeDescription		text  NOT NULL,
	BikeDateAdded		date  NOT NULL,
	BikePictName		varchar(64)
 ) 
GO

CREATE TABLE PurchasedTable ( 
	PurchaseSaleId		int primary key identity(1,1) NOT NULL,
	BikeId				int foreign key references BikeTable(BikeId) NOT NULL,
	PurchasedPrice      decimal NOT NULL,
--	PurchasedPrice      decimal(5,2)NOT NULL,
	PurchCustFirst		varchar(32) NOT NULL,
	PurchCustLast		varchar(64) NOT NULL,
	PurchCustPhone		varchar(16) NOT NULL,
	PurchCustAddress1	varchar(64) NOT NULL,
	PurchCustAddress2	varchar(64) NOT NULL,
	PurchCustCity		varchar(64) NOT NULL,
	PurchCustState		varchar(16) NOT NULL,
	PurchCustPostCode	varchar(16) NOT NULL,
	PurchFinType		varchar(32) NOT NULL
 ) 

 
CREATE TABLE FeatureTable ( 
	FeatureId			int primary key identity(1,1) NOT NULL,
	BikeId				int foreign key references BikeTable(BikeId) null,	
	FeatureDescription	varchar(256),
 ) 
GO

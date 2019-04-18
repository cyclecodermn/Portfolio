-- -  -   -    -     -      -       -        -
-- Note: We can't start in the normal fasion of looking for
-- and removing the db bcause Entity adds crtical info to the db
-- Instead, we remove the non-Entity tables.


USE [xperi]
GO


-- -  -   -    -     -      -       -        -
-- Remove Tables
IF EXISTS(SELECT * FROM sys.tables WHERE name='customers')
	DROP TABLE customers
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='feature')
	DROP TABLE feature
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='specials')
	DROP TABLE specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='sales')
	DROP TABLE sales
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='rolet')
	DROP TABLE rolet
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='users')
	DROP TABLE rolet
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='colort')
	DROP TABLE colort
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='modelt')
	DROP TABLE modelt
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='make')
	DROP TABLE make
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='bodystyle')
	DROP TABLE bodystyle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='vehicle')
	DROP TABLE vehicle
GO

-- -  -   -    -     -      -       -        -

CREATE TABLE bodystyle ( 
	bodyid               int primary key identity(1,1) not null,
	body                 char(64)
 ) 
GO

CREATE TABLE colort ( 
	colorid              int primary key identity(1,1) not null,
	color                char(32)
 )
GO 

CREATE TABLE customers ( 
	custid               int  NOT NULL  ,
	lastname             varchar(64)  NOT NULL  ,
	firstname            varchar(32)  NOT NULL  ,
	email                varchar(32)  NOT NULL
 ) 
GO

CREATE TABLE make ( 
	makeid               int primary key identity(1,1) not null,
	make                 varchar(32)  NOT NULL  ,
	userid               varchar(128)  NOT NULL  ,
)
GO

CREATE TABLE modelt ( 
	modelid              int primary key identity(1,1) not null,
	userid               varchar(128),
	makeId int foreign key references make(makeid) null,	
 ) 
GO

CREATE TABLE rolet ( 
	roleid               int  NOT NULL  ,
	role                 varchar(64),
	CONSTRAINT pk_roles_roleid PRIMARY KEY ( roleid )
 ) 

CREATE TABLE specials ( 
	specialid            int primary key identity(1,1) not null,
	title                varchar(48)  NOT NULL  ,
	description          varchar(256)
 ) 

CREATE TABLE vehicle ( 
	vehicleid            int primary key identity(1,1) not null,
	makeId int foreign key references make(makeid) null,	
	modelId int foreign key references modelt(modelId) null,	
	colorId int foreign key references colort(colorId) null,	
	bodyId int foreign key references bodystyle(bodyId) null,	
		
	msrp                 int  NOT NULL  ,
	[year]                 int  NOT NULL  ,
	isnew                binary(1) NOT NULL,
	isautom              binary(1) NOT NULL,
	miles                int NOT NULL, 
	vin                  char(20)  NOT NULL  ,
	description          text  NOT NULL  ,
	dateadded            date,
	pictname             char(64)
 ) 
GO

CREATE TABLE feature ( 
	featureid            int  NOT NULL  ,
	vehicleId int foreign key references vehicle(vehicleId) null,	
	description          varchar(256),
 ) 
GO

CREATE TABLE sales ( 
	saleid               int primary key identity(1,1) not null,
	vehicleId int foreign key references vehicle(vehicleId) null,	
	price                decimal  NOT NULL  ,
	purchtype            varchar(20),
 ) 


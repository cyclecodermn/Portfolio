
USE [master]
GO

if exists (select * from sys.databases where name = N'xperi')
begin
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'xperi';
	ALTER DATABASE xperi SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE xperi;
end

CREATE DATABASE [xperi]
GO

USE [xperi]
GO

-- -  -   -    -     -      -       -        -

CREATE TABLE bodystyle ( 
	bodyid               int primary key identity(1,1) not null,
	body                 char(64)
	RatingId int foreign key references Rating(RatingId) null,
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
	description          varchar(256),
 ) 

CREATE TABLE vehicle ( 
	vehicleid            int primary key identity(1,1) not null,
	makeId int foreign key references make(makeid) null,	
	modelId int foreign key references modelt(modelId) null,	
	colorId int foreign key references color(colorId) null,	
	bodyId int foreign key references body(bodyId) null,	
		
	msrp                 int  NOT NULL  ,
	[year]                 int  NOT NULL  ,
	isnew                binary(1),
	isautom              binary(1),
	miles                int,
	vin                  char(20)  NOT NULL  ,
	description          text  NOT NULL  ,
	dateadded            date,
	pictpath             char(256),
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


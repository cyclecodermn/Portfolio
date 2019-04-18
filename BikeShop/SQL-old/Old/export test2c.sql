USE [master]
GO

if exists (select * from sys.databases where name = N'xperi)
begin
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'xperi;
	ALTER DATABASE xperiSET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE xperi
end

CREATE DATABASE [xperi
GO

USE [xperi
GO

-- -  -   -    -     -      -       -        -

CREATE TABLE xperibodystyle ( 
	bodyid               int primary key identity(1,1) not null,
	body                 char(64)    ,
	CONSTRAINT pk_bodystyle_bodyid PRIMARY KEY ( bodyid )
 ) engine=InnoDB;

CREATE TABLE colort ( 
	colorid              int primary key identity(1,1) not null,
	color                char(32)    ,
	CONSTRAINT pk_colort_colorid PRIMARY KEY ( colorid )
 ) engine=InnoDB;

CREATE TABLE customers ( 
	custid               int  NOT NULL  ,
	lastname             varchar(64)  NOT NULL  ,
	firstname            varchar(32)  NOT NULL  ,
	email                varchar(32)  NOT NULL  ,
	CONSTRAINT pk_customers_custid PRIMARY KEY ( custid )
 ) engine=InnoDB;

CREATE TABLE make ( 
	makeid               int primary key identity(1,1) not null,
	make                 varchar(32)  NOT NULL  ,
	userid               varchar(128)  NOT NULL  ,
	CONSTRAINT pk_make_makeid PRIMARY KEY ( makeid )
 ) engine=InnoDB;

CREATE TABLE modelt ( 
	modelid              int primary key identity(1,1) not null,
	userid               varchar(128)    ,
	CONSTRAINT pk_modelt_modelid PRIMARY KEY ( modelid ),
	CONSTRAINT fk_modelt_make FOREIGN KEY ( modelid ) REFERENCES xperimake( makeid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;

CREATE TABLE rolet ( 
	roleid               int  NOT NULL  ,
	role                 varchar(64)    ,
	CONSTRAINT pk_roles_roleid PRIMARY KEY ( roleid )
 ) engine=InnoDB;

CREATE TABLE specials ( 
	specialid            int primary key identity(1,1) not null,
	title                varchar(48)  NOT NULL  ,
	description          varchar(256)    ,
	CONSTRAINT pk_specials_specialid PRIMARY KEY ( specialid )
 ) engine=InnoDB;

CREATE TABLE vehicle ( 
	vehicleid            int primary key identity(1,1) not null,
	msrp                 int  NOT NULL  ,
	year                 int  NOT NULL  ,
	isnew                binary(1)    ,
	isautom              binary(1)    ,
	miles                int    ,
	vin                  char(20)  NOT NULL  ,
	description          text  NOT NULL  ,
	dateadded            date   DEFAULT CURRENT_DATE ,
	pictpath             char(256)    ,
	CONSTRAINT pk_vehicle_vehicleid PRIMARY KEY ( vehicleid ),
	CONSTRAINT fk_vehicle_make FOREIGN KEY ( vehicleid ) REFERENCES xperimake( makeid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_vehicle_modelt FOREIGN KEY ( vehicleid ) REFERENCES xperimodelt( modelid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_interior_vehicle_colort FOREIGN KEY ( vehicleid ) REFERENCES xpericolort( colorid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_exterior_vehicle_colort FOREIGN KEY ( vehicleid ) REFERENCES xpericolort( colorid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_vehicle_bodystyle FOREIGN KEY ( vehicleid ) REFERENCES xperibodystyle( bodyid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;

CREATE TABLE feature ( 
	featureid            int  NOT NULL  ,
	description          varchar(256)    ,
	CONSTRAINT pk_featured_featureid PRIMARY KEY ( featureid ),
	CONSTRAINT fk_feature_vehicle FOREIGN KEY ( featureid ) REFERENCES xperivehicle( vehicleid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;

CREATE TABLE sales ( 
	saleid               int primary key identity(1,1) not null,
	price                decimal  NOT NULL  ,
	purchtype            varchar(20)    ,
	CONSTRAINT pk_sales_saleid PRIMARY KEY ( saleid ),
	CONSTRAINT fk_sales_vehicle FOREIGN KEY ( saleid ) REFERENCES xperivehicle( vehicleid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;


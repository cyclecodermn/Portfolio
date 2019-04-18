--- int primary key identity(1,1) not null

CREATE SCHEMA sgcars1;

CREATE TABLE sgcars1.bodystyle ( 
	bodyid               int  NOT NULL  AUTO_INCREMENT,
	body                 char(64)    ,
	CONSTRAINT pk_bodystyle_bodyid PRIMARY KEY ( bodyid )
 ) engine=InnoDB;

CREATE TABLE sgcars1.colort ( 
	colorid              int  NOT NULL  AUTO_INCREMENT,
	color                char(32)    ,
	CONSTRAINT pk_colort_colorid PRIMARY KEY ( colorid )
 ) engine=InnoDB;

CREATE TABLE sgcars1.make ( 
	makeid               int  NOT NULL  AUTO_INCREMENT,
	make                 varchar(32)  NOT NULL  ,
	userid               varchar(128)  NOT NULL  ,
	CONSTRAINT pk_make_makeid PRIMARY KEY ( makeid )
 ) engine=InnoDB;

CREATE TABLE sgcars1.modelt ( 
	modelid              int  NOT NULL  AUTO_INCREMENT,
	userid               varchar(128)    ,
	CONSTRAINT pk_modelt_modelid PRIMARY KEY ( modelid ),
	CONSTRAINT fk_modelt_make FOREIGN KEY ( modelid ) REFERENCES sgcars1.make( makeid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;

CREATE TABLE sgcars1.vehicle ( 
	vehicleid            int  NOT NULL  AUTO_INCREMENT, 
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
	CONSTRAINT fk_vehicle_make FOREIGN KEY ( vehicleid ) REFERENCES sgcars1.make( makeid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_vehicle_modelt FOREIGN KEY ( vehicleid ) REFERENCES sgcars1.modelt( modelid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_interior_vehicle_colort FOREIGN KEY ( vehicleid ) REFERENCES sgcars1.colort( colorid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_exterior_vehicle_colort FOREIGN KEY ( vehicleid ) REFERENCES sgcars1.colort( colorid ) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT fk_vehicle_bodystyle FOREIGN KEY ( vehicleid ) REFERENCES sgcars1.bodystyle( bodyid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;


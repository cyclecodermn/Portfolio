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

CREATE TABLE sgcars1.customers ( 
	custid               int  NOT NULL  ,
	lastname             varchar(64)  NOT NULL  ,
	firstname            varchar(32)  NOT NULL  ,
	email                varchar(32)  NOT NULL  ,
	CONSTRAINT pk_customers_custid PRIMARY KEY ( custid )
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

CREATE TABLE sgcars1.rolet ( 
	roleid               int  NOT NULL  ,
	role                 varchar(64)    ,
	CONSTRAINT pk_roles_roleid PRIMARY KEY ( roleid )
 ) engine=InnoDB;

CREATE TABLE sgcars1.specials ( 
	specialid            int  NOT NULL  AUTO_INCREMENT,
	title                varchar(48)  NOT NULL  ,
	description          varchar(256)    ,
	CONSTRAINT pk_specials_specialid PRIMARY KEY ( specialid )
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

CREATE TABLE sgcars1.feature ( 
	featureid            int  NOT NULL  ,
	description          varchar(256)    ,
	CONSTRAINT pk_featured_featureid PRIMARY KEY ( featureid ),
	CONSTRAINT fk_feature_vehicle FOREIGN KEY ( featureid ) REFERENCES sgcars1.vehicle( vehicleid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;

CREATE TABLE sgcars1.sales ( 
	saleid               int  NOT NULL  AUTO_INCREMENT,
	price                decimal  NOT NULL  ,
	purchtype            varchar(20)    ,
	CONSTRAINT pk_sales_saleid PRIMARY KEY ( saleid ),
	CONSTRAINT fk_sales_vehicle FOREIGN KEY ( saleid ) REFERENCES sgcars1.vehicle( vehicleid ) ON DELETE NO ACTION ON UPDATE NO ACTION
 ) engine=InnoDB;


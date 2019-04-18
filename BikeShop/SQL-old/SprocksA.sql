USE GuildCars1
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

-- -  -   -    -     -      -       -        -

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM BikeFrameTable;
	DELETE FROM SpecialTable;
	DELETE FROM BikeColorTable;
	DELETE FROM BikeMakeTable;
	DELETE FROM BikeModelTable;
-- -  -   -    -     -      -       -        -
	SET IDENTITY_INSERT BikeFrameTable ON;
	
	INSERT INTO BikeFrameTable (BikeFrameId, BikeFrame)
	VALUES
	(1,'Touring'),
	(2,'Road'),
	(3,'Hybrid');

	SET IDENTITY_INSERT BikeFrameTable OFF;

-- -  -   -    -     -      -       -        -
	SET IDENTITY_INSERT SpecialTable ON;
	
	INSERT INTO SpecialTable (SpecialId, SpecialTitle, SpecialDescription)
	VALUES
	(1,'Summer Sale','With summer here, it is a perfect time to try a new bike.'),
	(2,'Fall Color Clearance', 'The leaves are falling and so are our prices.'),
	(3,'Santa Special','Come in and talk with Santa about bringing a bike down your chimney.');

	SET IDENTITY_INSERT SpecialTable OFF;
-- -  -   -    -     -      -       -        -
	SET IDENTITY_INSERT BikeColorTable ON;
	
	INSERT INTO BikeColorTable (BikeColorId, BikeColor)
	VALUES
	(1,'White'),
	(2,'Light Grey'),
	(3,'Grey'),
	(4,'Dark Grey'),
	(5,'Charchol');

	SET IDENTITY_INSERT BikeColorTable OFF;
-- -  -   -    -     -      -       -        -
	SET IDENTITY_INSERT BikeMakeTable ON;
	
	INSERT INTO BikeMakeTable (BikeMakeId, BikeMake)
	VALUES
	(1,'Giant'),
	(2,'Surley'),
	(3,'Trek');

	SET IDENTITY_INSERT BikeMakeTable OFF;
-- -  -   -    -     -      -       -        -
	
	SET IDENTITY_INSERT BikeModelTable ON;
	
	INSERT INTO BikeModelTable (BikeModelId, BikeMakelId, BikeModel)
	VALUES
	(1,1,'RidgeBack'),
	(2,2,'Long Haul Trucker'),
	(3,3,'520');

	SET IDENTITY_INSERT BikeModelTable OFF;
-- -  -   -    -     -      -       -        -

END
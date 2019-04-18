USE GuildCars1
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

-- -  -   -    -     -      -       -        -

CREATE PROCEDURE DbReset AS
BEGIN

	DELETE FROM PurchasedTable;
	DELETE FROM BikeTable;
	DELETE FROM BikeFrameTable;
	DELETE FROM SpecialTable;
	DELETE FROM BikeColorTable;
	DELETE FROM BikeMakeTable;
	DELETE FROM BikeModelTable;

	DELETE FROM AspNetUsers;
	DELETE FROM BikeTable;
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
	INSERT INTO AspNetUsers(Id, firstName, lastName, EmailConfirmed, PhoneNumberConfirmed, Email,TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('00000000-0000-0000-0000-000000000000', 'James', 'Carter', 0, 0, 'test@test.com', 0, 0, 0, 'test');
-- -  -   -    -     -      -       -        -
	SET IDENTITY_INSERT BikeTable ON;

	INSERT INTO 
	BikeTable(BikeId,BikeMakeId,BikeModelId,BikeFrameColorId,BikeTrimColorId,BikeFrameId,BikeMsrp,BikeListPrice,BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikeDateAdded,BikePictName)
	VALUES 
	(1,1,1,1,1,1,1000.00,990.00,2012,1,2,18,12345678,'Good used condition',GETDATE(),'LongHaulTruckerPic1.jpg'),
	(2,2,2,2,2,2,2000.00,8800.00,2012,3,4,18,23456789,'Very ok',GETDATE(),'Bike2Pic.jpg');

	SET IDENTITY_INSERT BikeTable OFF;
-- -  -   -    -     -      -       -        -

	SET IDENTITY_INSERT PurchasedTable ON;

	INSERT INTO
	PurchasedTable(PurchaseSaleId,BikeId,PurchasedPrice,PurchCustFirst,PurchCustLast,PurchCustPhone,PurchCustAddress1,PurchCustAddress2,PurchCustCity,PurchCustState,PurchCustPostCode,PurchFinType)
	VALUES
	(1,1,1000,'Sandy','Mier','651-123-0987','111 Grand Ave','','St. Paul','MN','55105','Cash');

	SET IDENTITY_INSERT PurchasedTable OFF;

END
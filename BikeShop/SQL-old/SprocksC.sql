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

 DBCC CHECKIDENT('BikeTable', RESEED, 1)
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
	BikeTable(BikeId,BikeMakeId,BikeModelId,BikeFrameColorId,BikeTrimColorId,BikeFrameId,
	BikeMsrp,BikeListPrice,BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,
	BikeDescription,BikeDateAdded,BikePictName)
	VALUES 
	(1,1,1,1,1,1,1000.00,990.00,2019,1,10,18,12345678,'Fresh out of the box',GETDATE(),'LongHaulTruckerPic1.jpg'),
	(2,2,2,2,2,2,2000.00,800.00,2012,0,4,18,23456789,'Very ok',GETDATE(),'Bike2Pic.jpg');

	SET IDENTITY_INSERT BikeTable OFF;
-- -  -   -    -     -      -       -        -

	SET IDENTITY_INSERT PurchasedTable ON;

	INSERT INTO
	PurchasedTable(PurchaseSaleId,BikeId,PurchasedPrice,PurchCustFirst,PurchCustLast,PurchCustPhone,PurchCustAddress1,PurchCustAddress2,PurchCustCity,PurchCustState,PurchCustPostCode,PurchFinType)
	VALUES
	(1,1,1000,'Sandy','Mier','651-123-0987','111 Grand Ave','','St. Paul','MN','55105','Cash');

	SET IDENTITY_INSERT PurchasedTable OFF;

END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BikeInsert')
		DROP PROCEDURE BikeInsert
GO


CREATE PROCEDURE BikeInsert (
	@BikeId				int output,
	@BikeMakeId			int,
	@BikeModelId		int,	
	@BikeFrameColorId	int,	
	@BikeTrimColorId	int,
	@BikeFrameId		int,
	@BikeMsrp			money,
	@BikeListPrice		money,
	@BikeYear			int,
	@BikeIsNew			binary(1),
	@BikeCondition		int,
	@BikeNumGears		int,
	@BikeSerialNum		char(20),
	@BikeDescription	text,
	@BikeDateAdded		date,
	@BikePictName		varchar(64)
) AS
BEGIN
	INSERT INTO BikeTable (BikeMakeId,BikeModelId,BikeFrameColorId,BikeTrimColorId,BikeFrameId,BikeMsrp,BikeListPrice,BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikeDateAdded,BikePictName)
	
	VALUES (@BikeMakeId, @BikeModelId, @BikeFrameColorId, @BikeTrimColorId, @BikeFrameId, @BikeMsrp, @BikeListPrice, @BikeYear, @BikeIsNew, @BikeCondition, @BikeNumGears, @BikeSerialNum, @BikeDescription, @BikeDateAdded, @BikePictName);
	
	SET @BikeId=SCOPE_IDENTITY();
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BikeUpdate')
		DROP PROCEDURE BikeUpdate
GO

CREATE PROCEDURE BikeUpdate (
	@BikeId				int,
	@BikeMakeId			int,
	@BikeModelId		int,	
	@BikeFrameColorId	int,	
	@BikeTrimColorId	int,
	@BikeFrameId		int,
	@BikeMsrp			money,
	@BikeListPrice		money,
	@BikeYear			int,
	@BikeIsNew			binary(1),
	@BikeCondition		int,
	@BikeNumGears		int,
	@BikeSerialNum		char(20),
	@BikeDescription	text,
	@BikeDateAdded		date,
	@BikePictName		char(64)
) AS
BEGIN
	UPDATE BikeTable SET
		BikeMakeId			= @BikeMakeId,
		BikeModelId			= @BikeModelId,
		BikeFrameColorId	= @BikeFrameColorId,
		BikeTrimColorId		= @BikeTrimColorId,
		BikeFrameId			= @BikeFrameId,
		BikeMsrp			= @BikeMsrp,
		BikeListPrice		= @BikeListPrice,
		BikeYear			= @BikeYear,
		BikeIsNew			= @BikeIsNew,
		BikeCondition		= @BikeCondition,
		BikeNumGears		= @BikeNumGears,
		BikeSerialNum		= @BikeSerialNum,
		BikeDescription		= @BikeDescription,
		BikeDateAdded		= @BikeDateAdded,
		BikePictName		= @BikePictName
	WHERE BikeId = @BikeId;
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BikeDelete')
		DROP PROCEDURE BikeDelete
GO
-- -  -   -    -     -      -       -        -
 
CREATE PROCEDURE BikeDelete (
	@BikeId int
) AS
BEGIN
	BEGIN TRANSACTION
	
	DELETE FROM PurchasedTable WHERE BikeId = @BikeId;
	DELETE FROM FeatureTable WHERE BikeId = @BikeId;
	DELETE FROM BikeTable WHERE BikeId = @BikeId;
	
	COMMIT TRANSACTION
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BikeSelect')
		DROP PROCEDURE BikeSelect
GO
-- -  -   -    -     -      -       -        -

CREATE PROCEDURE BikeSelect (
	@BikeId int
) AS
BEGIN
SELECT BikeMakeId,BikeModelId,BikeFrameColorId,BikeTrimColorId,BikeFrameId,BikeMsrp,BikeListPrice,BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikePictName
	FROM BikeTable
	WHERE BikeId = @BikeId
END
GO


USE GuildCars1

-- -  -   -    -     -      -       -        -
-- I followed the video to create the procedure below
-- but I probably won't use it since it relates more to
-- ShackUp than Guild Bikes. The FeaturedBike procedure 
-- is derived from the 1 below and is used.
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BikesRecentlyAdded')
		DROP PROCEDURE BikesRecentlyAdded
GO

-- During a stable state of this project, try removing the procedure below.
CREATE PROCEDURE BikesRecentlyAdded AS
BEGIN
	SELECT TOP 5 BikeFrameId, BikeFrameName
	FROM BikeFrameTable
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'OneBikeDetails')
		DROP PROCEDURE OneBikeDetails
GO
-- -  -   -    -     -      -       -        -

CREATE PROCEDURE OneBikeDetails (
	@BikeId int
) AS
BEGIN

SELECT	BikeMakeName,BikeModelName, c.BikeColor AS frameColor,  
		ct.BikeColor AS trimColor, BikeFrameName,BikeMsrp,BikeListPrice,
		BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikePictName
	FROM BikeTable bt
		INNER JOIN BikeMakeTable mk ON mk.BikeMakeId = bt.BikeMakeId
		INNER JOIN BikeModelTable md ON md.BikeModelId = bt.BikeModelId 
		INNER JOIN BikeFrameTable fr ON fr.BikeFrameId = bt.BikeFrameId 

		INNER JOIN BikeColorTable c ON c.BikeColorId = bt.BikeFrameColorId
		INNER JOIN BikeColorTable ct ON ct.BikeColorId = bt.BikeTrimColorId
		
	WHERE BikeId = @BikeId
END

GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BikeSelectAll')
		DROP PROCEDURE BikeSelectAll
GO
-- -  -   -    -     -      -       -        -

CREATE PROCEDURE BikeSelectAll 
AS
BEGIN

SELECT	BikeId,BikeMakeName,BikeModelName, c.BikeColor AS frameColor,  
		ct.BikeColor AS trimColor, BikeFrameName,BikeMsrp,BikeListPrice,
		BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikePictName
	FROM BikeTable bt
		INNER JOIN BikeMakeTable mk ON mk.BikeMakeId = bt.BikeMakeId
		INNER JOIN BikeModelTable md ON md.BikeModelId = bt.BikeModelId 
		INNER JOIN BikeFrameTable fr ON fr.BikeFrameId = bt.BikeFrameId 

		INNER JOIN BikeColorTable c ON c.BikeColorId = bt.BikeFrameColorId
		INNER JOIN BikeColorTable ct ON ct.BikeColorId = bt.BikeTrimColorId
END

GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsSelectAll')
		DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT SpecialId, SpecialTitle, SpecialDescription
	FROM SpecialTable
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FramesSelectAll')
		DROP PROCEDURE FramesSelectAll
GO

CREATE PROCEDURE FramesSelectAll AS
BEGIN
	SELECT BikeFrameId, BikeFrameName
	FROM BikeFrameTable
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FrameSelect')
		DROP PROCEDURE FrameSelect
GO

CREATE PROCEDURE FrameSelect (
	@BikeFrameId int

) AS
BEGIN
	SELECT BikeFrameId, BikeFrameName
	FROM BikeFrameTable
	WHERE BikeFrameId = @BikeFrameId

END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'FrameInsert')
      DROP PROCEDURE FrameInsert
GO

CREATE PROCEDURE FrameInsert (
	@FrameId int output,
	@BikeFrame nvarchar(32)
) AS
BEGIN
	INSERT INTO BikeFrameTable(BikeFrameName,FrameAddedDate)
	VALUES (@BikeFrame,GETDATE());

	SET @FrameId=SCOPE_IDENTITY();
END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'FrameDelete')
      DROP PROCEDURE FrameDelete
GO

CREATE PROCEDURE FrameDelete (
	@BikeFrameId int

) AS
BEGIN
	DELETE FROM BikeFrameTable
	WHERE BikeFrameId = @BikeFrameId
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FrameUpdate')
		DROP PROCEDURE FrameUpdate
GO

CREATE PROCEDURE FrameUpdate (
	@BikeFrameId			int,
	@BikeFrameName		char(64)
) AS
BEGIN
	UPDATE BikeFrameTable SET
		BikeFrameName	= @BikeFrameName
	WHERE BikeFrameId	= @BikeFrameId;
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ColorsSelectAll')
		DROP PROCEDURE ColorsSelectAll
GO

CREATE PROCEDURE ColorsSelectAll AS
BEGIN
	SELECT BikeColorId, BikeColor
	FROM BikeColorTable
END
GO


-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ColorDelete')
      DROP PROCEDURE ColorDelete
GO

CREATE PROCEDURE ColorDelete (
	@BikeColorId int

) AS
BEGIN
	DELETE FROM BikeColorTable
	WHERE BikeColorId = @BikeColorId
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ColorUpdate')
		DROP PROCEDURE ColorUpdate
GO

CREATE PROCEDURE ColorUpdate (
	@BikeColorId			int,
	@BikeColorName			char(64)
) AS
BEGIN
	UPDATE BikeColorTable SET
		BikeColor	= @BikeColorName
	WHERE BikeColorId	= @BikeColorId;
END
GO

-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ColorInsert')
      DROP PROCEDURE ColorInsert
GO

CREATE PROCEDURE ColorInsert (
	@ColorId int output,
	@BikeColorName nvarchar(32)
) AS
BEGIN
	INSERT INTO BikeColorTable(BikeColor,ColorAddedDate)
	VALUES (@BikeColorName,GETDATE());

	SET @ColorId=SCOPE_IDENTITY();
END
GO
-- -  -   -    -     -      -       -        -



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ColorSelect')
		DROP PROCEDURE ColorSelect
GO

CREATE PROCEDURE ColorSelect (
	@BikeColorId int

) AS
BEGIN
	SELECT BikeColorId, BikeColor
	FROM BikeColorTable
	WHERE BikeColorId = @BikeColorId

END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelDelete')
      DROP PROCEDURE ModelDelete
GO

CREATE PROCEDURE ModelDelete (
	@BikeModelId int

) AS
BEGIN
	DELETE FROM BikeModelTable
	WHERE BikeModelId = @BikeModelId
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelUpdate')
		DROP PROCEDURE ModelUpdate
GO

CREATE PROCEDURE ModelUpdate (
	@BikeModelId			int,
	@BikeModelName			char(64)
) AS
BEGIN
	UPDATE BikeModelTable SET
		BikeModelName	= @BikeModelName
	WHERE BikeModelId	= @BikeModelId;
END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelSelect')
		DROP PROCEDURE ModelSelect
GO

CREATE PROCEDURE ModelSelect (
	@BikeModelId int

) AS
BEGIN
	SELECT BikeModelId, BikeModelName
	FROM BikeModelTable
	WHERE BikeModelId = @BikeModelId

END
GO
-- -  -   -    -     -      -       -        -


-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelsSelectAll')
		DROP PROCEDURE ModelsSelectAll
GO

CREATE PROCEDURE ModelsSelectAll AS
BEGIN
	SELECT BikeModelId, BikeModelName
	FROM BikeModelTable
END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN

	DELETE FROM ContactTable;
	DELETE FROM FeatureTable;
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
 DBCC CHECKIDENT('BikeModelTable', RESEED, 1)
 DBCC CHECKIDENT('BikeMakeTable', RESEED, 1)
 DBCC CHECKIDENT('BikeFrameTable', RESEED, 1)

 DBCC CHECKIDENT('ContactTable', RESEED, 1)

-- -  -   -    -     -      -       -        -
delete from BikeFrameTable;
	SET IDENTITY_INSERT BikeFrameTable ON;
	
	INSERT INTO BikeFrameTable (BikeFrameId, BikeFrameName)
	VALUES
	(1,'Touring'),
	(2,'Road'),
	(3,'Hybrid'),
	(4,'Banana'),
	(5,'City');


	SET IDENTITY_INSERT BikeFrameTable OFF;

-- -  -   -    -     -      -       -        -
delete from SpecialTable
	SET IDENTITY_INSERT SpecialTable ON;
	
	INSERT INTO SpecialTable (SpecialId, SpecialTitle, SpecialDescription)
	VALUES
	(1,'Summer Sale','With summer here, it is a perfect time to try a new bike.'),
	(2,'Fall Color Clearance', 'The leaves are falling and so are our prices.'),
	(3,'Santa Special','Come in and talk with Santa about bringing a bike down your chimney.');

	SET IDENTITY_INSERT SpecialTable OFF;
-- -  -   -    -     -      -       -        -
delete from BikeColorTable ;
	SET IDENTITY_INSERT BikeColorTable ON;
	
	INSERT INTO BikeColorTable (BikeColorId, BikeColor)
	VALUES
	(1,'Red'),
	(2,'Yellow'),
	(3,'Blue'),
	(4,'Green'),
	(5,'Orange'),
	(6,'Purple'),
	(7,'White'),
	(8,'Black'),
	(9,'Grey');

	SET IDENTITY_INSERT BikeColorTable OFF;
-- -  -   -    -     -      -       -        -
delete from BikeMakeTable;
	SET IDENTITY_INSERT BikeMakeTable ON;
	
	INSERT INTO BikeMakeTable (BikeMakeId, BikeMakeName)
	VALUES
	(1,'Giant'),
	(2,'Surley'),
	(3,'Trek'),
	(4,'Schwinn');

	SET IDENTITY_INSERT BikeMakeTable OFF;
-- -  -   -    -     -      -       -        -
	delete from BikeModelTable;
	SET IDENTITY_INSERT BikeModelTable ON;
	
	INSERT INTO BikeModelTable (BikeModelId, BikeMakelId, BikeModelName)
	VALUES
	(1,1,'Long Haul Trucker'),
	(2,2,'RidgeBack'),
	(3,3,'520');

	SET IDENTITY_INSERT BikeModelTable OFF;
-- -  -   -    -     -      -       -        -
--delete from aspnetusers;
	INSERT INTO AspNetUsers(Id, firstName, lastName, EmailConfirmed, PhoneNumberConfirmed, 
	Email,TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES
	('00000000-0000-0000-0000-000000000000', 'James', 'Carter', 1, 1, 'test@test.com', 0, 0, 0, 'test'),
	('41cc81c5-5290-4bc5-864c-dfa43f2f8020', 'Sue', 'David', 1, 1, 'sue@sgul.ac.uk', 0, 0, 0, 'sue');


-- -  -   -    -     -      -       -        - 
delete from biketable;
	SET IDENTITY_INSERT BikeTable ON;

	INSERT INTO 
	BikeTable(BikeId,BikeMakeId,BikeModelId,BikeFrameColorId,BikeTrimColorId,BikeFrameId,
	BikeMsrp,BikeListPrice,BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,
	BikeDescription,BikeDateAdded,BikePictName)
	VALUES 
	(1,2,1,2,1,1,1111.00,1100.00,2019,1,10,18,1111111,'Fresh out of the box',GETDATE(),'bike-pic (0).jpg'),
	(2,3,2,3,2,3,2222.00,2200.00,2019,1,9,18,2222222,'Scratch from the factory',GETDATE(),'bike-pic (1).jpg'),
	(3,2,3,2,3,2,333.00,300.00,2012,0,4,1,3333333,'Very ok',GETDATE(),'50s Hiawatha.jpg'),
	(4,1,3,1,3,1,444.00,400.00,2004,1,4,1,4444444,'Description 4',GETDATE(),'bike-pic (3).jpg'),
	(5,1,2,3,1,2,555.00,500.00,1975,0,5,1,5555555,'Description 5',GETDATE(),'Schwinn stingray.jpg'),
	(6,2,3,1,2,3,666.00,600.00,2006,1,6,1,666666,'Description 6',GETDATE(),'bike-pic (5).jpg'),
	(7,3,1,2,3,1,777.00,700.00,1983,0,7,1,777777,'Description 7',GETDATE(),'Schwinn World Tourist.jpg'),
	(8,1,2,3,1,2,888.00,800.00,2008,1,8,1,888888,'Description 8',GETDATE(),'bike-pic (7).jpg'),
	(9,2,3,1,2,3,200.00,185.00,1978,0,9,1,999999,'Description 9',GETDATE(),'Schwinn Le Tour.jpg'),
	(10,3,2,1,3,3,100.00,10.00,2010,1,10,1,101010,'Description 10',GETDATE(),'bike-pic (9).jpg');

	SET IDENTITY_INSERT BikeTable OFF;

-- -  -   -    -     -      -       -        -
delete from FeatureTable;

	SET IDENTITY_INSERT FeatureTable ON;
	
	INSERT INTO FeatureTable (FeatureId, BikeId, FeatureDescription)
	VALUES
	(1,1,'This is a description for featured bike number 1'),
	(2,2,'This is a description for featured bike number 2'),
	(3,3,'This is a description for featured bike number 3'),
	(4,8,'This is a description for featured bike number 4'),
	(5,10,'This is a description for featured bike number 5');

	SET IDENTITY_INSERT FeatureTable OFF;

-- -  -   -    -     -      -       -        -
delete from PurchasedTable;
	SET IDENTITY_INSERT PurchasedTable ON;

	INSERT INTO
	PurchasedTable(PurchaseSaleId,BikeId,PurchasedPrice,PurchCustFirst,PurchCustLast,PurchCustPhone,PurchCustAddress1,PurchCustAddress2,PurchCustCity,PurchCustState,PurchCustPostCode,PurchFinType)
	VALUES
	(1,1,1000,'Sandy','Mier','651-123-0987','111 Grand Ave','','St. Paul','MN','55105','Cash');

	SET IDENTITY_INSERT PurchasedTable OFF;

-- -  -   -    -     -      -       -        -
delete from ContactTable;
	SET IDENTITY_INSERT ContactTable ON;

	INSERT INTO 
	ContactTable(ContactId,CntctLastName, CntctFirstName, CntctPhone, CntctEmail,CntctMessage)
	VALUES
	(1,'Werner','Wally','111-111-1111','wally@werner.org','Message for contact 1'),
	(2,'Kent','Johnson','222-222-2222','kent@johnson.org','Message for contact 2')

	SET IDENTITY_INSERT ContactTable OFF;

	end 
	go
-- -  -   -    -     -      -       -        -
-- -  -   -    -     -      - Stored Procedures Below
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

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetFeaturedBikes')
		DROP PROCEDURE GetFeaturedBikes
GO

CREATE PROCEDURE GetFeaturedBikes AS
	BEGIN
		SELECT	FeatureId, bt.BikeId, BikeYear, BikeMakeName,BikeModelName, BikeListPrice, BikePictName
			FROM FeatureTable ft
				INNER JOIN BikeTable bt ON bt.BikeId = ft.BikeId
				INNER JOIN BikeMakeTable mk ON mk.BikeMakeId = bt.BikeMakeId
				INNER JOIN BikeModelTable md ON md.BikeModelId = bt.BikeModelId 
				INNER JOIN BikeFrameTable fr ON fr.BikeFrameId = bt.BikeFrameId 
	END
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNewOrUsedBikes')
		DROP PROCEDURE GetNewOrUsedBikes
GO

CREATE PROCEDURE GetNewOrUsedBikes(
	@NewOrUsed nvarchar(1)
)
AS
	BEGIN
		SELECT	bt.BikeId, BikeYear, BikeMakeName,BikeModelName, BikeListPrice, BikePictName
			FROM BikeTable bt
				INNER JOIN BikeMakeTable mk ON mk.BikeMakeId = bt.BikeMakeId
				INNER JOIN BikeModelTable md ON md.BikeModelId = bt.BikeModelId 
				INNER JOIN BikeFrameTable fr ON fr.BikeFrameId = bt.BikeFrameId 
	WHERE BikeIsNew = @NewOrUsed
	END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactInsert')
      DROP PROCEDURE ContactInsert
GO

CREATE PROCEDURE ContactInsert (
	@ContactId nvarchar(128),
	@CntctLastName varchar(64),
	@CntctFirstName varchar(32),
	@CntctPhone varchar(15),
	@CntctEmail varchar(32),
	@CntctMessage varchar(256)
) AS
BEGIN
	INSERT INTO Contacts(ContactId, CntctLastName, CntctFirstName, CntctPhone, CntctEmail,CntctMessage)
	VALUES (@ContactId, @CntctLastName,@CntctPhone,@CntctEmail,@CntctMessage)
END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactDelete')
      DROP PROCEDURE ContactDelete
GO

CREATE PROCEDURE ContactDelete (
	@ContactId nvarchar(128)

) AS
BEGIN
	DELETE FROM ContactTable
	WHERE ContactId = @ContactId
END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactsSelect')
      DROP PROCEDURE ContactsSelect
GO

CREATE PROCEDURE ContactsSelect (
	@ContactId nvarchar(128)
) AS
BEGIN
	SELECT ContactId, CntctLastName, CntctFirstName, CntctPhone, CntctEmail,CntctMessage
	FROM Contacts
	WHERE ContactId = @ContactId
END
GO

-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelInsert')
      DROP PROCEDURE ModelInsert
GO

CREATE PROCEDURE ModelInsert (
	@ModelId int output,
	@BikeModelName nvarchar(32)
) AS
BEGIN
	INSERT INTO BikeModelTable(BikeModelName,ModelAddedDate)
	VALUES  (@BikeModelName,GETDATE());

	SET @ModelId=SCOPE_IDENTITY();
END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeInsert')
      DROP PROCEDURE MakeInsert
GO

CREATE PROCEDURE MakeInsert (
	@MakeId int output,
	@BikeMakeName nvarchar(32)
) AS
BEGIN
	INSERT INTO BikeMakeTable(BikeMakeName,MakeAddedDate)
	VALUES (@BikeMakeName,GETDATE());

	SET @MakeId=SCOPE_IDENTITY();
END
GO
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeDelete')
      DROP PROCEDURE MakeDelete
GO

CREATE PROCEDURE MakeDelete (
	@MakeId int

) AS
BEGIN
	DELETE FROM MakeTable
	WHERE MakeId = @MakeId
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakesSelectAll')
		DROP PROCEDURE MakesSelectAll
GO

CREATE PROCEDURE MakesSelectAll AS
BEGIN
	SELECT BikeMakeId, BikeMakeName 
	FROM BikeMakeTable
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakeSelect')
		DROP PROCEDURE MakeSelect
GO

CREATE PROCEDURE MakeSelect (
	@BikeMakeId int

) AS
BEGIN
	SELECT BikeMakeId, BikeMakeName
	FROM BikeMakeTable
	WHERE BikeMakeId = @BikeMakeId

END
GO
-- -  -   -    -     -      -       -        -
-- -  -   -    -     -      -       -        -
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeDelete')
      DROP PROCEDURE MakeDelete
GO

CREATE PROCEDURE MakeDelete (
	@BikeMakeId int

) AS
BEGIN
	DELETE FROM BikeMakeTable
	WHERE BikeMakeId = @BikeMakeId
END
GO
-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakeUpdate')
		DROP PROCEDURE MakeUpdate
GO

CREATE PROCEDURE MakeUpdate (
	@BikeMakeId			int,
	@BikeMakeName			char(64)
) AS
BEGIN
	UPDATE BikeMakeTable SET
		BikeMakeName	= @BikeMakeName
	WHERE BikeMakeId	= @BikeMakeId;
END
GO

-- -  -   -    -     -      -       -        -
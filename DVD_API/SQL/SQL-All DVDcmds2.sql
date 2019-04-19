use dvdRepoEF
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdGetAll')
      DROP PROCEDURE dvdGetAll
GO
CREATE PROCEDURE dvdGetAll
AS
    SELECT *
    FROM dvdMain
    ORDER BY dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdGetById')
      DROP PROCEDURE dvdGetById
GO
CREATE PROCEDURE dvdGetById (@dvdId int)
AS
    SELECT *
    FROM dvdMain
    WHERE dvdId = @dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
  WHERE ROUTINE_NAME = 'dvdAdd')
     DROP PROCEDURE dvdAdd
GO

CREATE PROCEDURE dvdAdd (
@dvdId int output,
@dvdTitle nvarchar(128),
@dvdYear int,
@dvdDirector nvarchar(64),
@dvdRating varchar(4),
@dvdNotes nvarchar(600) = ''
)
AS
    INSERT INTO dvdMain (title, realeaseYear, director, rating, notes) values
    (@dvdTitle,@dvdYear,@dvdDirector,@dvdRating,@dvdNotes)

    set @dvdId = SCOPE_IDENTITY()
	-- A new ID was generated, and now, we're sending it back.
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdEdit')
      DROP PROCEDURE dvdEdit
GO
CREATE PROCEDURE dvdEdit (
    @dvdId int,
    @dvdTitle varchar (128),
    @dvdYear int,
    @dvdDirector varchar(64),
    @dvdRating varchar(4),
    @dvdNotes varchar(600)
)
AS
    UPDATE dvdMain
        SET title = @dvdTitle,
        realeaseYear = @dvdYear,
        director = @dvdDirector,
        rating = @dvdRating,
        notes = @dvdNotes
    WHERE dvdId = @dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdGetByTitle')
      DROP PROCEDURE dvdGetByTitle
GO
CREATE PROCEDURE dvdGetByTitle (@term varchar(50))
AS
    SELECT *
    FROM dvdMain
    Where title = @term
    ORDER BY dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdGetByYear')
      DROP PROCEDURE dvdGetByYear
GO
CREATE PROCEDURE dvdGetByYear (@term varchar(50))
AS
    SELECT *
    FROM dvdMain
    Where realeaseYear = @term
    ORDER BY dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdGetByDirector')
      DROP PROCEDURE dvdGetByDirector
GO
CREATE PROCEDURE dvdGetByDirector (@term varchar(50))
AS
    SELECT *
    FROM dvdMain
    Where director = @term
    ORDER BY dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdGetByRating')
      DROP PROCEDURE dvdGetByRating
GO
CREATE PROCEDURE dvdGetByRating (@term varchar(50))
AS
    SELECT *
    FROM dvdMain
    Where rating = @term
    ORDER BY dvdId
GO

-- -  -   -    -     -      -       -        -

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'dvdDelete')
      DROP PROCEDURE dvdDelete
GO
CREATE PROCEDURE dvdDelete (@dvdId int)
AS
    DELETE 
    FROM dvdMain
    WHERE dvdId = @dvdId
GO


-- -  -   -    -     -      -       -        -

USE DvdRepoEF
GO

if exists (select * from sys.server_principals where name = N'DvdLibraryApp')
drop login DvdLibraryApp
go

if exists (select * from sys.database_principals where name = N'DvdLibraryApp')
drop user DvdLibraryApp
go
 
CREATE LOGIN DvdLibraryApp WITH PASSWORD='Testing123'
GO

USE DvdRepoEF
GO
 
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO

-- Change param 1 to stored procedures and change DvdLibraryApp EVERYWHERE to DvdLibraryApp
GRANT EXECUTE ON dvdGetByDirector TO DvdLibraryApp
GRANT EXECUTE ON dvdGetById TO DvdLibraryApp
GRANT EXECUTE ON dvdAdd TO DvdLibraryApp
GRANT EXECUTE ON dvdEdit TO DvdLibraryApp
GRANT EXECUTE ON dvdDelete TO DvdLibraryApp
GRANT EXECUTE ON dvdGetByDirector TO DvdLibraryApp
GRANT EXECUTE ON dvdGetByRating TO DvdLibraryApp
GRANT EXECUTE ON dvdGetByTitle TO DvdLibraryApp
GRANT EXECUTE ON dvdGetByYear TO DvdLibraryApp
GRANT EXECUTE ON dvdGetAll TO DvdLibraryApp
GO


--Change movie to my table
GRANT SELECT ON dvdMain TO DvdLibraryApp
GRANT INSERT ON dvdMain TO DvdLibraryApp
GRANT UPDATE ON dvdMain TO DvdLibraryApp
GRANT DELETE ON dvdMain TO DvdLibraryApp
GO


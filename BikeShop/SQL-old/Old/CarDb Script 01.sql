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

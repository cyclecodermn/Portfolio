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


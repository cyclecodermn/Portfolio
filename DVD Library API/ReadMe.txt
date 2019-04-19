This project is a DVD library that utilizes SQL, ASP .NET, Web API, and website using HTML, JavaScript, and CSS. Additionally, the project uses ADO or Entity Framework to access the database, using a factory method that reads from a webconfig file.

This was an mid-course mastery project from the Software Guild. It was not a tutorial since we were given no instructions to follow when creating the project. Instead, I created the project from scratch using similar yet different projects.

Requirements of this project are listed below.

Database Build Scripts

You must provide repeatable scripts to create thedatabase,it’sobjects, and sample data. Please submit a .zip file named DvdScripts.zip containing these files: 
o DvdLibraryCreate.sql – Should create the database and its tables
o DvdLibrarySprocs.sql – Should create the stored procedures.
o DvdLibrarySampleData.sql – Should create sample data
o DvdLibrarySecurity.sql – Should create an application account.

Web Application Guidance
o The Web API application must be configured to use CORS. Allow all sites, origins, and methods (*).
o The Web API application should contain an IDvdRepository interface. There should be three implementations of this interface: 
     - DvdRepositoryMock – Returns in-memory sample data.
     - DvdRepositoryEF – Uses Entity Framework code-first to store and retrieve data from SQL Server.
     - DvdRepositoryADO – Uses ADO .NET to store and retrieve data from SQL Server.
o A factory class should instantiate the appropriate implementation of the interface. It should be driven by a web.config <appSettings> key called “Mode”.  Values of “Mode” are: 
     - SampleData – Selects DvdRepositoryMock
     - EntityFramework – Selects DvdRepositoryEF
     - ADO – Selects DvdRepositoryADO
o The DvdLibrarySecurity.sql file must perform the following actions. 
     - Create a server login named ‘DvdLibraryApp’ with a password of ‘testing123’.
     - Create a database account for ‘DvdLibraryApp’.
     - Grant Execute on all used stored procedures to ‘DvdLibraryApp’
     - Grant SELECT, INSERT, UPDATE, and DELETE on all used tables to ‘DvdLibraryApp’

Web API Specification
o You must provide the following functionality:
o Retrieving a Dvd by Id
o Retrieving all Dvds
o Retrieving Dvds by Title
o Retrieving Dvds by Release Year
o Retrieving Dvds by Director Name
o Retrieving Dvds by Rating
o Creating a new Dvd, using JSON
o Updating an Existing Dvd, using JSON
o Deleting a Dvd


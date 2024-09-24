
DATA Layer
	Anything related only to database
	Connection to database (See DbContext.cs)

	Childrens:
		*These are common folders acting as sub-layers in the DATA layer
		DTO - Data Transfer Object (View Model). Using Entities as reference and getting/using only what you need. Acts as the main object for use in services/functions
		Entities - 1:1 of Database/Table
		Migrations - For Code First, supposedly used as a directory in database-migrations related commands.
		Models - Is known and acts as Entities before. Acts as a generics folder upon return value of the services/controllers. Can be used to immediately see by value (bool) before printing the value.

Folderings (In DAL system architecture):

Abstract = Interface == IService
Concrete = Services == Service




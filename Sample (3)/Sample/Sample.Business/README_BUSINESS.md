
BUSINESS Layer
	Acts as the container for services
	Services: 
		Functions that usually uses DbContext for processing. 
		Usually returns the respective DTOs
			Or void (task<>) should it be just processes (DELETE, UPDATE, etc)
	Async is (almost) always used due to its simultaneous process (Nagaantayan)

	Children:
		Services - CRUD functions directly to database
		*An Interface folder is followed with the Services folder

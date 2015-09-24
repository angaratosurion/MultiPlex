---REQUIREMENTS---

SQL Server:
	If you do not wish to modify anything in the sample application, 
	you will need SQL Server 2008 Express. The application will automatically
	mount the App_Data\Wiki.mdf database and utilize it.

	If you don't have (or do not want to install) SQL Server 2008 Express,
	but have access to a SQL Server 2005/2008 instance - you can still
	use the sample application. 

	1. In the SQL Server instance, create a new database called 'Wiki'
	2. Execute the App_Data\Wiki.sql script
	3. Change the connection string 'WikiConnectionString' to point
	   to your SQL Server instance.
	   
Visual Studio:
	The sample application requires Visual Studio 2008. Running the sample
	application in debug mode will launch the built-in web server shipped
	with Visual Studio.
	   
IIS / ASP.NET:
	IIS is not required, however the sample application has been
	pre-built and would not require you to compile it with Visual
	Studio 2008. You will need the .NET Framework 3.5 installed, with
	ASP.NET 2.0 registered in IIS.
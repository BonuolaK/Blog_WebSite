# Blog_WebSite 

# Angular Project
The NwareBlog-Angular folder contains the front end SPA
Please run npm install and build to install dependent libaries

#Improvements
Create alert service and component to share across pages
Create interceptor for httpclient to filter out non 200 responses and throw unique error via error service

# .NET CORE API Project
The NwareBlog folder contains the backend web api
Build the project to download missing nuget packages such as Swagger and EF Core
To successfully run the application the database has to be created.
Change the default connection string in the appsettings.Development.json file to local system database connection string
Run update-database command using the Visual Studio Package Manager Console to run the migration scripts to selected MSSQL database
Running the app will load the Swagger UI API Documentation.

#Improvements
API - Add global error filter to log and handle exceptions caught and return friendly message to client.
Test - Add unit test with mock services to test controller classes.


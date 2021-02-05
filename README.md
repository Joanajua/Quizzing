"# Quizzing" 
NUGET PACKAGES NEEDED TO BUILD THE APPLICATION

The following Nuget packages are needed for the appiclation to run:

>>Quizzing.Web project

	○ Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation v3.1.10
	○ Microsoft.VisualStudio.Web.CodeGeneration.Design  v3.1.4
	○ Microsoft.EntityFrameworkCore.Design v3.1.11

	○ Microsoft.EntityFrameworkCore.Tools v3.1.11
	○ Npgsql.EntityFrameworkCore.PostgreSQL v3.1.1
	○ Microsoft.EntityFrameworkCore.Sqlite v3.1.10
	○ Microsoft.EntityFrameworkCore.SqlServer v3.1.11

	○ Microsoft.AspNetCore.Identity.EntityFrameworkCore v 3.1.10
	○ Microsoft.AspNetCore.Identity.UI v3.1.10
	
	○ Microsoft.AspNetCore.Mvc.ViewFeatures v2.2.0
	○ Microsoft.AspNetCore.Mvc.Core v 2.2.5
	
	○ FluentValidation v9.4.0
	○ FluentValidation.AspNetCore v9.4.0
	

>>Quizzing.UnitTest project

	○ Microsoft.NET.Test.Sdk v16.7.1
	○ xunit v2.4.1
	○ xunit.runner.visualstudio v2.4.3
	○ Microsoft.EntityFrameworkCore.InMemory  v3.1.10
	○ Microsoft.AspNetCore.TestHost v3.1.10
	○ coverlet.collector v1.3.0
	○ Moq v4.15.2

>>--------------------------
CHANGE POSTGRESS CONECTION STRING

>> Add own local postgres database id and password to the conection string in the appsettings.json file:
"AppConnection": "User ID=USERID;Password=PASSWORD;Host=localhost;Port=5432;Database=quizzing;"

Now run the program; when the application detects that the database does not exist,
it runs the migrations and create the database that will be built with the respective tables and sample data for quizzes.
There will be 3 quizes as an example with their own questions and answers and also an admin user, one admin role and one readonly role will be created.

>>--------------------------
FEED USERS AND ROLES

To feed the database with users, there are 3 CVS documents on the application root folder inside a folder named "Identity-datafiles".
1- Import the AspNetUsers file into the AspNetUsers table. When importing could throw an error, due to a timezone column, and postgres justs copies 2 users; in that case,
enter the other user by hand copying directly from the file.
2- Import the AspNetRoles file into the AspNetRoles table. This should not cause any problem.
3- Import the AspNetUserRoles file into the AspNetUserRoles table.

ACCESS TO THE APPLICATION BY LOGIN

Now there are 3 users created:

edit@webbiskools.com
view@webbiskools.com
restricted@webbiskools.com

The password for all of them is:
Abcd123456@

Each of the users is assigned to one role: edit, view or restricted; when the application starts, the UI shows directly the Login page.

>>--------------------------

DIFFERENT USER Roles

As per the busines requirements each user has a different grade of authorisation.

- Users with edit role will be able to access to the whole application.
- Users with view role will be able to see the list of quizzes and on each of them the list questions and answers.
- Users with restricted role will be able to see the list of quizzes and on each of them the list questions but not the answers.

"# Quizzing" 
RUN THE APPLICATION FOR THE FIRST TIME

>> Add own local postgres database id and password to the conection string in the appsettings.json file:
"AppConnection": "User ID=USERID;Password=PASSWORD;Host=localhost;Port=5432;Database=quizzing;"

When the application detects that the database does not exist,
it runs the migrations and create the database that will be built with the respective tables and sample data.
There will be 3 quizes as an example with their own questions and answers and also an admin user, one admin role and one readonly role will be created.

>>--------------------------

NUGET PACKAGES NEEDED TO BUILD THE APPLICATION

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

HOW TO ACCESS TO THE APPLICATION

An admin user assigned to the admin role will be created the first time the application runs
and has to be used to access the application as an administrator:

>> Go to Login and use the following credentials.
Email: admin@mailinator.com
Pasword: Abcd123456@

A read-only role (readonly) will also be generated at the start of the application.

To register a new user go to Register and enter an email and a valid password - this is at least
10 characteres with at least 1 uppercase and with at least one symbol.

When registering a new user, this will be assigned to the readonly role automatically;
if this user needs to be assigned to another role, an admin user with an admin role can
assign a new role to it inside the users and roles managment side of the application.

>> Go to Management > Roles > Create new role (if a new role needs to be created).
>> Go to Management > Users > Edit user > Manage Roles > select the new user and assign a new role to it.

>>--------------------------

DIFFERENT USER Roles

- Users with admin role will be able to access to the whole application.
- Users with readonly role will be able to login, access to the index page,
the list of quizzes and the Details page where they can see the questions and answers for a quiz.
- Users unregistered or unlogged can access to the login, resgister and Index page.
They can also access to the List of quizzes; when accessing to the Details page
they can see the questions for a quizz but not the answers.

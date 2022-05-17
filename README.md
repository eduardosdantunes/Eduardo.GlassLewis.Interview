# Eduardo.GlassLewis.Interview
.Net6 - Minimal API - GlassLewis Interview

---------------------------------------------------------------------------------------------------------------------------------------
# Challenge

Design and code a Web API solution in .NET Core/5+ and C# for a middle tier “Company API.”

Using this WebAPI, an end user should be able to:
 
1. Create a Company record specifying the Name, Stock Ticker, Exchange, ISIN, and optionally a website URL.
 
   a. You are not allowed create two Companies with the same ISIN.
  
   b. The first two characters of an ISIN must be letters / non numeric.
 
2. Retrieve an existing Company by Id
3. Retrieve a Company by ISIN
4. Retrieve a collection of all Companies
5. Update an existing Company
 

Sample company records:

| Name                 | Exchange             | Ticker | ISIN         | website                    |
|----------------------|----------------------|--------|--------------|----------------------------|
| Apple Inc.           | NASDAQ               | AAPL   | US0378331005 | http://www.apple.com       |
| British Airways Plc  | Pink Sheets          | BAIRY  | US1104193065 |                            |
| Heineken NV          | Euronext Amsterdam   | HEIA   | NL0000009165 |                            |
| Panasonic Corp       | Tokyo Stock Exchange | 6752   | JP3866800000 | http://www.panasonic.co.jp |
| Porsche Automobil    | Deutsche Börse       | PAH3   | DE000PAH0038 | https://www.porsche.com/   |

 
6. Code should be testable and have some level of unit test coverage.
7. It should run end to end and read and write to a database. 
8. Please also design the database you would need and provide all SQL scripts and source used to create the application.
9. If any additional steps are required to deploy or get the application running these should be documented very clearly.

Bonus points:

10. Provide a very simple client to call the API and present the results in a browser using any client-side web technology you like.

Even more points:

11. Add authentication code to secure the API.


# About the solution
Minimal-API

A functional API with some leve of test coverage using the new ASP.NET Core minimal APIs for hosting and HTTP APIs.
The minimal API of .net6 was chosen for the solution of this challenge in order to bring modernity in the construction of API with low code cost to the programmer.
During the project some challenges were found that were solved in palliative ways but will be described at the end of this document in "Future changes / Known issues"

You can check instructions on how to create your own minimal API at [Minimal APIs overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)

# Get Started

## Requirements

* If you want to perform debugging using microsoft visual studio you should follow these steps: [Install Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022)
* If you want to perform debugging or executing commands using console you should follow these steps: [How to Install .NET 6](https://docs.microsoft.com/en-us/dotnet/core/install/) and download and install [.NET Download](https://dotnet.microsoft.com/en-us/download)
* Get and install Docker following the documentation if needed in your environment: [Install Docker](https://docs.docker.com/get-docker/)
* Get and install Docker Compose following the documentation if needed in your environment: [Install docker-compose](https://docs.docker.com/engine/install/)
* The application will use ports 90 and 443 for the address 127.0.0.1 (localhost), so leave these ports free. 
  Not forgetting also port 5050 on which the database will be live. If necessary, make changes to these ports in the files:
  * Dofickerfile
  * docker-compose.yml
  * docker-compose.override.yml

  If you have doubts about the changes to the files, consult the manufacturer's documentation for [Docker reference](https://docs.docker.com/engine/reference/builder/) or for [docker-compose reference](https://docs.docker.com/compose/compose-file/)
  
## Steps to run

0. Download the project to your machine and go to the root path from this solution.

1. Build docker-compose and name the project "entreviewapi" by running the following command:
```sh
docker-compose -f docker-compose.yml -f docker-compose.override.yml -p interviewapi build
```

2. Execute the follwing command to put the project on the air.
```sh
docker-compose -p interviewapi up
```

3. Access your browser in the following URL: [My Running Application](https://localhost/swagger)

## Steps to test

1. (Easy Method) If you have Microsoft Visual Studio installed:
Just follow the steps in the image below:

![image](https://user-images.githubusercontent.com/105398346/168405542-3720e12b-112a-4686-912d-c8ef90e238e4.png)


2. Install at least the .NET6 SDK described in "requirements", go to the desired tests directory (Example: "./tests/Interview.Domain.Tests") and run the following command in the console.

```sh
donet test
```

You should get the following result 

![image](https://user-images.githubusercontent.com/105398346/168405613-a25c5d4c-bcc5-42f3-867f-89694f3ca438.png)



## Useful information

This solution will up TWO containers:
* Minimal API Application (It will be running on localhost on port 443 by default)
* Database (It will be running on localhost on port 5050 by default)

If you want to access the database and check the design table applied to the exercise, just connect to the database using the username and password as shown in the image below:

* default user: sa
* default password: YourStrong@Passw0rd
  
![image](https://user-images.githubusercontent.com/105398346/168388712-de5cac12-f134-4835-b25a-e70f69665e07.png)

You will also be able to access the database script that is stored in the db folder at the root of the project with the name [script.sql](https://github.com/eduardosdantunes/Eduardo.GlassLewis.Interview/blob/main/db/script.sql)

# Future changes / Known issues

## Improved ISIN field validation

Studying the correct implementation of ISIN, I believe that this can be improved and explored through code validations that would not only check the amount of characters and strings starting with 2 letters but also with the check digit.

[How Does the ISIN Numbering System Work?](https://www.investopedia.com/ask/answers/06/isinnumberingsystem.asp)

Solved using PR [Commit - ISIN field validation](https://github.com/eduardosdantunes/Eduardo.GlassLewis.Interview/commit/dcb6038bf689c41830fc2d1c48880e5a1ca714ff)

- [x] DONE

## - API filter application on ISIN query endpoint 

.NET 6 doesn't have filter feature yet but it is coming soon. With that, the endpoint
```sh
/api/v1/companies/isin/{isin}
```
Was implemented in this way as a Workaround knowing that the correct way would be through the implementation of filter in the get of the endpoint 
```sh
/api/v1/companies/isin/{isin}
```

Here are articles that commenting on what's to come in .NET7 contemplating API Filters andwTwo open issues for the implementation of Filters in APIs:

* [.NET7 preview with API Filters](https://visualstudiomagazine.com/articles/2022/04/18/aspnetcore-net-7-preview-3.aspx)
* [.NET github API Filter issue 1](https://github.com/dotnet/aspnetcore/issues/37853)
* [.NET github API Filter issue 2](https://github.com/dotnet/aspnetcore/issues/40506)

- [ ] DONE

## Create integrated tests

It is true that the application would have to have more integrated tests to guarantee the quality at the time of API calls, cruds in the database, among other integrations that can be applied.

- [ ] DONE

# Related and Reference projects

Here are some used documentation related to this project:

* Microsoft Minimal API Documentation

   https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0

* Entity Framework Core

   https://docs.microsoft.com/en-us/ef/core/

* MinimalApiCrud

   https://github.com/diegostan/MinimalApiCrud

* A complete and functional Minimal API using .NET 6

   https://github.com/EduardoPires/Minimal-API/


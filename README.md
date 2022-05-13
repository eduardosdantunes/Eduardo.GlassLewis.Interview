# Eduardo.GlassLewis.Interview
.Net6 - Minimal API - GlassLewis Interview

---------------------------------------------------------------------------------------------------------------------------------------

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





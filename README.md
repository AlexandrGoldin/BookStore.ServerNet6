# BookStore.ServerNet6

### C# .Net Core 6 WebApi. This backend  application interacts with the frontend BookStore.Client.ForNet6 application. https://github.com/AlexandrGoldin/BookStore.Client.ForNet6
### BookStoreNet6 of the Client-Server application of the online store emulator for PC. 
___
### Stack:
* MS SQL Server
* TSQL
* Entity Framework Core for Commands
* Dapper for Queries
* N-tier architecture
* ASP.NET Core 6
* Web API
* CORS
* CQRS Mediatr library
* Global exception handler middleware
* Serilog. "WriteTo": [{"Name": "Console"}, {"Name": "File", "Args": {"path": "./Logs/log-.txt", ...}}]
* Swashbuckle
* Data Transfer Object
* AutoMapper
* FluentValidation
* MemoryCache
* AddApiVersioning Microsoft.AspNetCore.Mvc.Versioning
* xUnit. Unit tests, integration tests, functional tests. 
___
#### The main page ![the main page](https://github.com/user-attachments/assets/7a39eaf1-f124-46c5-8d47-b8fcd3211ca1)
______
#### Method GET /api/{version}/Users  Gets the list of products(books) ![GET api version Users](https://github.com/user-attachments/assets/1b4ffd97-9b9b-4e6a-8bf3-755776070cbb)
____


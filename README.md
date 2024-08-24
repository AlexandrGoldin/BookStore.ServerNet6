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
* Microsoft.AspNetCore.Mvc.Versioning
* xUnit. Unit tests, integration tests, functional tests. 
___
#### Swagger-UI URL https//localhost:5001/index.html ![Localhost 5001 index html](https://github.com/user-attachments/assets/14b930ec-c8cd-4a51-a892-4448935c1343)
______
#### Method GET /api/{version}/Users/Index  
#### Gets the list of products(books) ![GET api version Users](https://github.com/user-attachments/assets/1b4ffd97-9b9b-4e6a-8bf3-755776070cbb)
____
#### Method GET /api/{version}/Admin/Index  
#### Gets the list of orders ![GET api version Admin Index](https://github.com/user-attachments/assets/2054c255-9231-4fea-ab1c-4ef35da631bc)


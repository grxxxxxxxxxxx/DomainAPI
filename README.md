```text
$$$$$$$\             $$$$$$\            $$\                         
$$  __$$\           $$$ __$$\           $$ |                        
$$ |  $$ | $$$$$$\  $$$$\ $$ | $$$$$$\  $$ | $$$$$$\                
$$$$$$$  |$$  __$$\ $$\$$\$$ |$$  __$$\ $$ |$$  __$$\               
$$  ____/ $$$$$$$$ |$$ \$$$$ |$$ /  $$ |$$ |$$$$$$$$ |              
$$ |      $$   ____|$$ |\$$$ |$$ |  $$ |$$ |$$   ____|       
$$ |      \$$$$$$$\ \$$$$$$  /$$$$$$$  |$$ |\$$$$$$$\   
\__|       \_______| \______/ $$  ____/ \__| \_______|      $$$$$$\ 
                              $$ |                          \______|
                              $$ |                                  
                              \__|


< Projects of this author are open for use by anyone for any purpose. >

This <api> has many disadvantages and vulnerabilities, and therefore does not claim to be a #finished product.
--> Author is also open to your pull request in order to improve the project. <--                      
```
DomainAPI - This is a REST API for domain name rentals, which is based on a Minimal API for development speed.
The following technology stack is used within this API ->
- [ ] Clean Architecture Monolit ( Services / DTOs )
- [ ] ASP.NET Core Minimal API
- [ ] PostgreSQL
- [ ] JWT + Role Authorization ( endpoints through the role )
- [ ] Docker + Docker Compose
- [ ] FluentValidation
- [ ] Serilog
- [ ] Swagger
- [ ] Integration tests ( XUnit )

The detailed structure of the DomainAPI is shown below.
> You can use it to study the structure and other aspects in more detail.


-> Warning: this structure does not include a separate project with integration tests.

```text
DomainAPI/
├── Endpoints/ # Minimal API Endpoints
│ ├── Auth/
│ ├── Domains/
│ ├── GetInfo/
│ └── Order/
│
├── Services/ # Logic
│ ├── Result/
│ ├── UserService.cs
│ ├── DomainService.cs
│ ├── LoginSerivice.cs
│ └── OrderService.cs
│ 
├── Models/ # Entities for the database
│ ├── Domain.cs
│ ├── LoginRequest.cs
│ ├── Users
│ └── Order.
│
├── logs/ #Serilog
│ 
├── DTOs/ # Data Transfer Objects
│ ├── Auth/
│ ├── DomainDto.cs
│ ├── DomainResponseDto.cs
│ ├── OrderDto.cs
│ └── OrderResponseDto.cs
│
├── Data/ # Working with data
│  └── AppDbContext.cs
│
├── Migrations/
│
├── FluentValidation/ # Data Validation
│ ├── RegisterValid.cs
│ ├── DomainValid.cs
│ ├── OrderValid.cs
│ └── LoginValid.cs
│
├── Dockerfile # Docker Configuration
├── docker-compose.yml # Docker Compose Configuration
└── README.md # Documentation
```
## Launching in Docker
To deploy the API on your machine, just follow 3 simple steps.

1 -> Cloning repository
```
git clone https://github.com/Pe0ple-push/DomainAPI.git
```
2 -> Go to project directory
```
cd DomainAPI
```
3 -> Lift container
```
docker-compose up -d
```
Warning => before the last step, you should make sure that Docker is installed on your device !

## Swagger Documentation
The container does not automatically apply database migrations !
Before you start -> apply the migrations as follows:
```
cd BookingProject
```
```
dotnet ef migrations add NameMigrations
```
```
dotnet ef database update
```
After the successful application of migrations to the database in our container - you can proceed to work with the API.

>Warning: migration should be applied after every container restart !
Or insert a script to automatically apply migrations.

To test the endpoints and logic after successfully launching the container and the database in it, go to localhost ->
```
http://localhost:8080/swagger
```
Or, instead of Swagger, you can use Curl directly in your terminal, an example of a login request is ->
```
curl -X POST http://localhost:8080/api/login \
  -H "Content-Type: application/json" \
  -d '{"email": "user@mail.com", "password": "Password123!"}'
```
> If an error occurs in the incoming parameters ( 400 -> Bad Request" - check the validity of the data you entered.

## Role endpoints
Most API requests require you to be logged in as a user, otherwise request will fail.
In order for system to identify your profile, you need to add your JWT to request.
```
curl -X DELETE \
  'http://localhost:8080/api/users/123e4567-e89b-12d3-a456-426614174000' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c'
```
Above you see an example of such a request , which includes JWT for user identification ( who makes the request to the protected endpoint ).
The other endpoints work on the same principle.

## Integration tests
Integration tests will help to check the API and our endpoints.
To run integration tests you need to do a few simple steps ->
```
cd DomainsTests
```
```
dotnet test
```
>Warning: Before running the tests manually, enable a container named <booking_tests> in Docker !

Detailed test report using Serilog<Loging>:
```
[17:41:47 WRN] Incorrect user data -> test123123@gmail.com
[17:41:47 INF] User create process -> test123123@gmail.com
[17:41:47 WRN] Incorrect user data -> test123123@gmail.com
[17:41:47 INF] User create process -> test92756@example.com
[17:41:47 WRN] Incorrect user data -> test92756@example.com
[17:41:47 INF] User create process -> test123123@gmail.com
[17:41:47 WRN] Incorrect user data -> test123123@gmail.com
[17:41:48 INF] Successful user creation -> test3d12d4541ef44e3599f25a651066263b@example.com
[17:41:48 INF] Successful user creation -> test81e3561af68b48e8ab78cee76cd22c17@example.com
[17:41:48 INF] User create process -> test15b3d60cdd514640a44bd9eedcdf750e@example.com
[17:41:48 INF] Login attempt test81e3561af68b48e8ab78cee76cd22c17@example.com
[17:41:48 INF] Successful user creation -> test15b3d60cdd514640a44bd9eedcdf750e@example.com
[17:41:48 WRN] Incorrect password test81e3561af68b48e8ab78cee76cd22c17@example.com
[17:41:48 INF] Login attempt test15b3d60cdd514640a44bd9eedcdf750e@example.com
[17:41:49 INF] Create JWT test15b3d60cdd514640a44bd9eedcdf750e@example.com
[17:41:49 INF] Successful login test15b3d60cdd514640a44bd9eedcdf750e@example.com
[xUnit.net 00:00:01.59]   Finished:   DomainsTests
  DomainsTests (test) net10.0 successfully completed (2.0c)

Test summary: total: 13; failure: 0; successful: 13; missed: 0; duration: 2.0 s
Assembly successfully completed with warnings (25) within 5.6 s
```
If testing is done by someone else - you can track the outcome of the tests in the container logs!

## Fluent Validator { Exception }
Before starting any work and testing - carefully check the conditions of built-in validators in services!
This will avoid phantom errors related to non-qualified data.

All negative scripts and error handling are present in the source code of integration tests ( users ).

## Summary & author’s words
This API and documentation including - are not overproductive and advanced - no controllers, global error handling, automatic application migration, user token update and much more.

But it can give a starting point for projects with authorization and booking logic , which will be overgrown after some time in production ready API.

>Project written by user #pe0ple






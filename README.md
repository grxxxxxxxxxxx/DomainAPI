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
- [ ] Integration tests

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






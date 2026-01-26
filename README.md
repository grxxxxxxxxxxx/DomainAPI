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
```

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





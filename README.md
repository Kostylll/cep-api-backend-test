# ViaCepAPI Project

Third-Party API Documentation

Este projeto utiliza a ViaCEP API para consulta de endereços.
Documentação completa: https://viacep.com.br

## Project Overview
ViaCepAPI is a .NET 8 Web API project that allows users to perform operations on address data.  
The API integrates with the **ViaCEP service** to fetch address information based on CEP (Brazilian postal codes).  
It includes authentication via **JWT tokens**, uses **AutoMapper** for DTO mapping, and follows **SOLID** principles for clean code.

### Features
- Search addresses by CEP using ViaCEP API
- JWT-based authentication
- AutoMapper mapping between entities and DTOs
- Structured error handling and validation

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- MySQL or SQLite database (depending on your setup)
- Postman (optional, for testing endpoints)

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/Kostylll/junior-backend-assessment.git

3. Navigate to the project folder:
   ```bash
   cd ViaCepAPI
  
4.Restore dependencies:
  ```bash
  dotnet restore
   ```
5.Configure the database connection in appsettings.json.
```bash
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=ViaCepDb;User=root;Password=123456;"
    },
    "Jwt": {
      "Key": "SUA_CHAVE_SECRETA",
      "Issuer": "ViaCepAPI",
      "Audience": "ViaCepAPI"
    }
  }
```
6. Apply Migrations (Open Infra.Data on the terminal to run the commands)
   dotnet ef migrations add InitialCreate
   dotnet ef database update

8.Executar a aplicação
  dotnet run

9.Acessar a documentação Swagger
 http://localhost:{PORT}/swagger/index.html

License
MIT License © Pedro Vanderlei

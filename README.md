# ViaCepAPI Project

Third-Party API Documentation: [ViaCEP](https://viacep.com.br)

---

## Project Overview

**ViaCepAPI** is a .NET 8 Web API project that allows users to perform operations on address data.  
The API integrates with the **ViaCEP** service to fetch address information based on **CEP (Brazilian postal codes)**.  

Key features:
- Search addresses by CEP using ViaCEP API
- JWT-based authentication
- AutoMapper mapping between entities and DTOs
- Structured error handling and validation
- Follows SOLID principles for clean code

---

## Features

- **Search addresses by CEP** using the ViaCEP API
- **Authentication** via JWT tokens
- **DTO mapping** with AutoMapper
- **Error handling** and validation for safer operations

---

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022
- MySQL or SQLite database
- Postman (optional, for testing endpoints)

### Steps

1. **Clone the repository**
```bash
git clone https://github.com/Kostylll/junior-backend-assessment.git
   git clone https://github.com/Kostylll/junior-backend-assessment.git
   ```
2 Navigate to the project folder:
   ```bash
   cd ViaCepAPI
  ```
3.Restore dependencies:
  ```bash
  dotnet restore
   ```
4.Configure the database connection in appsettings.json and ViaCepDbContext.
```bash
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=ViaCepDb;User=root;Password=123456;"
    },
    "Jwt": {
      "Key": "your_secret_key",
      "Issuer": "ViaCepAPI",
      "Audience": "ViaCepAPI"
    }
  }
```
⚠️ Replace Password with your own database password.

```bash
var cnn = "Data Source=localhost;Database=ViaCepDb;uid=root;password=your_password!";
```
5. Apply Migrations (run in Infra.Data project)
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. Run the application
 ```bash
  dotnet run
```
7.Access Swagger documentation
  ```bash
 http://localhost:{PORT}/swagger/index.html
   ```

### Authentication

To use the API, you first need to create a user and then log in to obtain an authentication token.

1. Create a User
Send a request to:
 ```bash
https://localhost:44347/api/Auth/createUser
```
Provide a valid email and a password.
The password is securely stored using BCrypt.
Make sure to remember your password, as it will be required for login.

2. Login
Once the user is created, log in with your credentials at:
 ```bash
https://localhost:44347/api/Auth/login
```
If the login is successful, the API will return a JWT token.

3. Using the Token
Copy the returned token.
Use it to authorize your requests in Swagger or tools like Postman.
After authorization, you are free to test and interact with all available endpoints in the ViaCep API.

License
MIT License © Pedro Vanderlei

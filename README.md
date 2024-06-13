# Project Compilation and Execution Instructions

## Backend (ASP.NET Core)

1. **Open Solution (.sln)**
   - Open the solution file (.sln) in Visual Studio.

2. **Run Backend Server**
   - Start the backend server by running the main project.
   - Ensure that the backend server is running correctly.

## Frontend (Angular)

1. **Navigate to Frontend Directory**
   - Open a terminal or command prompt and navigate to the frontend project directory.

2. **Install Dependencies**
   - Before the first run, ensure Angular CLI and npm are installed.
   - Install necessary packages by running:
     ```
     npm install
     ```

3. **Serve the Angular App**
   - Start the Angular development server with live reload by running:
     ```
     ng serve
     ```
   - Once compiled, a local server address will be displayed in the console.

4. **Accessing the Application**
   - Open a web browser and navigate to the provided local server address (usually `http://localhost:4200`).
   - The Angular application should now be running and accessible.

## Swagger API Documentation

- **Access Swagger UI**
  - While the backend server is running, you can access Swagger UI for detailed API documentation.
  - Navigate to the Swagger endpoint, typically located at:
    ```
    http://localhost:{7037}/swagger
    ```
  - Here, all API methods will be listed and documented for easy reference.


# Used Technologies

In this project, the following technologies were used:

- **Angular**: Frontend framework for building dynamic web applications.
- **ASP.NET Core**: Backend framework for building web APIs and applications.
- **Entity Framework Core**: Object-relational mapping (ORM) framework for .NET Core.
- **Identity Framework**: Framework for managing users, roles, and authentication in ASP.NET applications.
- **JwtBearer Authentication**: Middleware for ASP.NET Core to support JSON Web Token (JWT) authentication.
- **SQL Server**: Relational database management system used for storing application data.
- **SQL Server Management Studio (SSMS)**: Integrated environment for managing SQL Server databases and objects.
- **Swagger**: API documentation tool used to describe and document ASP.NET Core APIs.

# Project Structure

## Backend (`ContactsWebApp`)

- 📁 **Controllers**
  - AuthController.cs - handles REST API registration and login
  - ContactsController.cs - handles REST API actions for contacts (get all contacts, get contact by id, create new contact, edit contact, delete contact)
  
- 📁 **DTO**
  - ContactDto.cs - DTO of contact model
  - LoginDto.cs - DTO of login model
  - UserRoles.cs - roles of app user for Identity Framework
  
- 📁 **Migrations** - Entity Framework migrations
  
- 📁 **Properties**
  - launchSettings.json - backend server launch settings
  
- 📁 **Repositories**
  - AuthRepository.cs - handles DB operations for register and login
  - IAuthRepository.cs - interface for AuthRepository
  - ContactsRepository.cs - handles DB operations for Contacts collection
  - IContactsRepository.cs - interface for ContactsRepository
  
- 📁 **Services**
  - AuthService.cs - handles non-DB operations for register and login
  - IAuthService.cs - interface for AuthService
  - ContactsService.cs - handles non-DB operations for Contacts collection
  - IContactsService.cs - interface for ContactsService
  
- Program.cs - startup class for backend server
- appsettings.json - app settings such as DB connection string

## Frontend (`Frontend`)

- 📁 **src**
  - 📁 **app**
    - 📁 contact-add-page - module for adding contacts
    - 📁 contact-edit-page - module for editing contacts
    - 📁 contact-page - module for displaying contact details
    - 📁 environments
      - environment.ts - backend configuration for domain and apiUrl
    - 📁 header - module for navbar header
    - 📁 home - module for home page
    - 📁 interceptors
      - error.interceptor.ts - interceptor for handling bad request error response from server 
    - 📁 login - module for login page
    - 📁 models
      - contact.ts - contact model
    - 📁 register - module for register page
    - 📁 services
      - auth.service.ts - service for requests register and login request to backend server
      - contacts.service.ts - service for requests for contacts (delete, edit, add, get)
  - app-routing.module.ts - routing config
  - app.module.ts - modules injection

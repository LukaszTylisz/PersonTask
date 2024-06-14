## PersonTask

This is repository for a web application built with `ASP.NET Core 8 Web API`.  includes instructions for setting up the development environment, running the application and managing database migrations with Entity Framework Core. It's a personal application where data are added by using Bogus and user can manage data using CRUD actions.

## Requirements

Before you begin, ensure you have the following software installed on your system:

- [.NET Core 8 SDK]
- [Node.js]
- [Vue.js]

## Development Setup
1. Clone this repository:
   ```bash
   https://github.com/LukaszTylisz/PersonTask.git
1. Create a database and apply migrations:
   ```bash
   Add-Migration InitialCreate
   
   Update-Database
2. cd Client
    ```bash
    npm install
3. npm run serve
4. Back end run on: https://localhost:7000/ and front end on: https://localhost:8080

### Solution structure

![image](https://github.com/LukaszTylisz/PersonTask/assets/86656091/9cf17022-d224-4d7e-9e71-19ae174d0706)


### Swagger

![image](https://github.com/LukaszTylisz/PersonTask/assets/86656091/d0494b43-d11b-42fc-a209-7cd5639e94e8)


### Frontend

![image](https://github.com/LukaszTylisz/PersonTask/assets/86656091/9bd17c6c-81cf-4842-a38a-07e4bc0ba75b)



### Architecture overview

With Clean Architecture, the `Domain` and `Application` layers are at the center of the design. This is known as the `Core` of the system. 

The Domain layer contains enterprise logic and types and the Application layer contains business logic and types.
The Core shouldn’t be dependent on concerns such as `Infrastructure`, so we invert those dependencies.

This is achieved by adding interfaces and abstractions within Core, which are implemented by layers outside Core such as Infrastructure. Front end site is made by Vue.js and it's connecting with backend using the WEB API where are implemented CRUD operations.All data is saved in MSSQL database.
Frontend is fetching data from database where user can refresh data, add/edit/delete person.

All dependencies flow inwards, and Core has no dependencies on any other layers.
Infrastructure and Presentation depend on Core, but not on one another.

#### The architecture is based on the following architectural principles:
- Separation of concerns
- Dependency Inversion
- Single Responsibility
- DRY

This results in a design that is: 
- Independent of UI, databases, frameworks
- Clean, maintainable, testable

### Technologies
- ASP.NET Core 8 Web API
- Onion Architecture
- CQRS with MediatR
- Fluent Validation
- Automapper
- Bogus
- Feature‑based folder organization
- Entity Framework Core
- Repository Pattern
- Swagger UI
- In-Memory Database for Integration Tests

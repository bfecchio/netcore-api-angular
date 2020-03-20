# .NET Core + EF Core + Angular 9 (w/ Angular Material)

This project is an example of architecture using new technologies and best practices.
The goal is to share knowledge and use it as reference for new projects.

## Technologies

* [.NET Core 3.1](https://dotnet.microsoft.com/download)
* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core)
* [Entity Framework Core 3.1](https://docs.microsoft.com/en-us/ef/core)
* [C# 8.0](https://docs.microsoft.com/en-us/dotnet/csharp)
* [Angular 9](https://angular.io/docs)
* [Angular CLI](https://cli.angular.io/)
* [Angular Material](https://material.angular.io/) 
* [Typescript](https://www.typescriptlang.org/docs/home.html)
* [HTML](https://www.w3schools.com/html)
* [CSS](https://www.w3schools.com/css)
* [JWT](https://jwt.io)
* [FluentValidation](https://fluentvalidation.net)
* [AutoMapper](https://automapper.org/)
* [Scrutor](https://github.com/khellang/Scrutor)
* [IdentityServer4](https://github.com/IdentityServer/IdentityServer4)
* [Serilog](https://serilog.net)
* [Docker](https://docs.docker.com)
* [Node](https://nodejs.org/)

## Practices

* Clean Code
* SOLID Principles
* DDD (Domain-Driven Design)
* Code Analysis
* Separation of Concerns
* Unit of Work Pattern
* Repository Pattern
* Database Migrations
* Authentication
* Authorization
* Logging

## Prerequisites

* [.NET Core SDK](https://aka.ms/dotnet-download)
* [Node.js](https://nodejs.org)
* [Angular CLI](https://cli.angular.io)

## Getting Started

1. Clone this repository

   ```bash
   git clone https://github.com/bfecchio-dev/test-fullstack.git
   cd test-fullstack
   ```

2. Build and run .NET Core WebAPI

    ```bash
    cd source/backend
    dotnet restore
    dotnet build
    dotnet run --project FullStack.Api
    ```

3. Build and run Angular 9 application

   ```bash
   cd source/frontend   
   npm install
   ng serve
   ```

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

To log in to the application, use the following credentials **(username: admin / password: admin@2020).**
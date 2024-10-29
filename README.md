# Team Counters

Team Counters is a web API that allows users to manage and track different counters
grouping them in teams. The API allows users to create counters and teams, manage
teams by adding or removing counters to it and get information about accumulated
counter values by teams and counters.

This project was created as a coding challenge for a job application.

> Initially, the mental model of the project implied that the counters are step counters which are distributed among employees which are grouped in some teams. However currently the API doesn't have any restriction on this except that the counter values are positive integers. So, using this API you can put any meanings in the 'Counter' and 'Team' terms according to your needs.

## Build and Run
To build and run the solution you will need:
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Open solution directory and run dotnet CLI:
```
dotnet run --project .\src\Web.Api\
```

Open [https://localhost:7221/swagger/index.html](https://localhost:7221/swagger/index.html) link to get access to Swagger UI of this project.

## Architecture

The application is built using ASP.NET 8 and C# language using a controller-based approach. This approach was chosen mostrly due to the application author's prior experience and familiarity with this style.

The project is architected using the Clean Architecture pattern, a well-established approach that promotes modularity, testability, and maintainability.
As there are many different interpretations of the Clean Architecture, the author used the one proposed by [Jason Taylor](https://github.com/jasontaylordev) in many of his presentations at various conferences and embodied in his [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture) GitHub repository.
There are many ideas was taken from his repository, but the project is not a direct copy of it and the solution was not based on a project template from this repository.

The whole solution is divided into the following layers:
* Domain: Contains domain entities and some interfaces which are used in the other layers.
* Application: Contains use cases. This project is based on CQRS approach using the [MediatR](https://github.com/jbogard/MediatR) library.
* Infrastructure: Contains data access and other infrastructure-related code.
* Web.Api: Contains the API controllers and other web-related code.

### Domain
Domain model of the application consists of two main entities: 'Counter' and 'Team'. Each of them has a unique identifier and a name.
The 'Counter' can be created independent of 'Team' and can belong to only one 'Team' or none. The 'Team' can contain zero or multiple 'Counters'. As long as and only as long as the 'Counter' is a part of any 'Team', its count value is considered as part of the total value of this 'Team'. If the 'Counter' is removed from the 'Team' its count value is also removed from the total value of the 'Team' and can be moved to another 'Team'.

### Application

Application layer based on CQRS approach developed using MediatR library. Some vertical slicing architecture ideas are used to separate parts of this layer. Among others it means that all queries and commands related to 'Counter' are placed in the 'Counter' folder and all queries and commands related to 'Team' are placed in the 'Team' folder. FluentValidation library is used for validation of the requests.

Due to simplicity the current realization the application layer uses simple mappings between domain entities and DTOs. In the real-world application, it can be replaced with some more advanced mapping libraries like AutoMapper.

### Infrastructure
At the moment this layer is included only very simple custom-made in-memory storage. Theoretically it can be replaced with a real database access layer with only some minor changes in other parts of the solution. Data access is implemented using the very simple repository pattern.

## Technologies
* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)

## What Can Be Improved

There are many things that can be improved in this project. Some of them are:
* Add tests. One of the main principles of the Clean Architecture is testability. However, the project doesn't have any tests at the moment.
* Add more validation. The project uses FluentValidation library for validation, but not all cases are covered.

## License
This project is licensed under the GPLv3 License - see the [LICENSE](LICENSE) file for details.

![Build Status](https://travis-ci.com/profjordanov/realworld-microservices.svg?branch=master)

# ![RealWorld Example App](https://raw.githubusercontent.com/profjordanov/realworld-microservices/master/docs/logo.png)

> ### A Microservices ASP.NET Core codebase containing real world examples (CRUD, auth, advanced patterns, etc) that adheres to the [RealWorld](https://github.com/gothinkster/realworld) spec and API.


### [RealWorld Repo](https://github.com/gothinkster/realworld)


This codebase was created to demonstrate a fully-fledged, micro-service architecture built with **ASP.NET Core**. It includes gRPC, Domain-Driven Design, CQRS, Mediator, Proxy, and many more patterns.

It completely adheres to the **ASP.NET Core** community style guides & best practices.

For more information on how to this works with other frontends/backends, head over to the [RealWorld](https://github.com/gothinkster/realworld) repo.

# Features

- [x] Microservices architecture - architectural style that provides a highly maintainable, testable, loosely coupled collection of services that are independently deployable and organized around business capabilities. Service layer is placed on top of te Domain Models. 

- [x] Domain-Driven Design - guides us to focus on small, individual, nearly autonomous pieces of our domain, our process and the resulting software is more flexible. We can easily move or modify the small parts with no side effects.

- [x] Communication via gRPC - a new, growing way to connect services in a cross-platform, cross-language way. 

- [x] Remote Proxy - acts like a local resource while hiding the details of how to connect to a remote resource over a network. It behaves as API gateway between the client and services.

- [x] Applying Functional Principles - Functional programming in C# can give you insight into how your programs will behave. Specific topics here are immutable architecture, avoiding exceptions, primitive obsession, how to handles failures and input errors, and more.

- [x] Command-Query Responsibility Segregation (CQRS) - encourages you to untangle a single, unified domain model and create two models: one for handling commands, and the other one for handling queries. CQRS allows us to make different decisions for reads and writes, which in turn brings three benefits: scalability, performance, and the biggest one, simplicity. CQRS extends CQS to the architectural level.

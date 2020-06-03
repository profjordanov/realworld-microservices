![Build Status](https://travis-ci.com/profjordanov/realworld-microservices.svg?branch=master)

# ![RealWorld Example App](logo.png)

> ### A Microservices ASP.NET Core codebase containing real world examples (CRUD, auth, advanced patterns, etc) that adheres to the [RealWorld](https://github.com/gothinkster/realworld) spec and API.


### [RealWorld Repo](https://github.com/gothinkster/realworld)


This codebase was created to demonstrate a fully-fledged, micro-service architecture built with **ASP.NET Core**. It includes gRPC, Domain-Driven Design, CQRS, Mediator, Proxy, and many more patterns.

It completely adheres to the **ASP.NET Core** community style guides & best practices.

For more information on how to this works with other frontends/backends, head over to the [RealWorld](https://github.com/gothinkster/realworld) repo.

# Features

- [x] Microservices architecture

 The backend is structured as a collection of web services that are:
  - Highly maintainable and testable
  - Loosely coupled
  - Independently deployable
  - Organized around business requirements
  
- [x] Communication via gRPC  

- [x] Remote Proxy ( acts like a local resource while hiding the details of how to connect to a remote resource over a network )

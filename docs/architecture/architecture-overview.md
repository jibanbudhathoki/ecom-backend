# Architecture Overview

This project implements a Clean Architecture modular monolith.

## Layers

- **Domain**: Core business logic and entities.
- **Application**: Application services, use cases, and validation.
- **Infrastructure**: Database access, external APIs, and cross-cutting concerns.
- **Api**: The public-facing HTTP API.
- **Admin**: An internal back-office application.

## Principles

- **Dependency Inversion**: Dependencies point inward toward the Domain.
- **Separation of Concerns**: Each layer has a specific responsibility.
- **Testability**: The design allows for easy unit and integration testing.

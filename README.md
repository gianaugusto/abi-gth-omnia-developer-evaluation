# Developer Evaluation Project

## Overview

This project is a developer evaluation for the DeveloperStore team. The goal is to implement an API that handles sales records, following the Domain-Driven Design (DDD) principles and the External Identities pattern with denormalization of entity descriptions.

![.NET Build](https://github.com/gianaugusto/abi-gth-omnia-developer-evaluation/actions/workflows/dotnet.yml/badge.svg?branch=main)

See [Overview](/.doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

## Use Case

The API needs to handle sales records and provide information such as:
- Sale number
- Date when the sale was made
- Customer
- Total sale amount
- Branch where the sale was made
- Products
- Quantities
- Unit prices
- Discounts
- Total amount for each item
- Cancelled/Not Cancelled

### Business Rules

- Purchases above 4 identical items have a 10% discount
- Purchases between 10 and 20 identical items have a 20% discount
- It's not possible to sell above 20 identical items
- Purchases below 4 items cannot have a discount

## Setup Instructions

### Prerequisites

- Docker
- Docker Compose

### Installation

1. **Install Docker**:
   Follow the instructions on the [Docker website](https://www.docker.com/get-started) to install Docker on your machine.

2. **Install Docker Compose**:
   Follow the instructions on the [Docker Compose website](https://docs.docker.com/compose/install/) to install Docker Compose.

### Running the Project

1. **Clone the Repository**:
   ```sh
   git clone https://github.com/gianaugusto/abi-gth-omnia-developer-evaluation.git
   cd abi-gth-omnia-developer-evaluation
   ```

2. **Build and Run the Containers**:
   ```sh
	docker-compose build
	docker-compose up
	 ```

3. You should be able to browse different components of the application by using the below URLs :

```sh
	Web Api			: http://localhost:8080/
	Log Dashboard   : http://localhost:9000/
``` 


4. **Run Migrations**:	 
In case of running in a non Development environment is needed to **Run Migrations** otherwise the schema will be created automatically
   ```sh
   dotnet ef migrations add InitialCreate --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi
   dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi
   ```


### Explanation of Rules

- **Create**: The API allows creating new sales records.
- **Use**: The API provides endpoints to retrieve, update, and delete sales records.
- **Authenticate**: The API includes authentication mechanisms to secure access to the endpoints.

## Project Structure

The project is organized into several directories, each serving a specific purpose. For a detailed description of the project structure, see the [Project Structure](/.doc/project-structure.md) documentation.

## API Structure

The API is structured into different resources. For detailed documentation on each resource, see the following links:
- [API General](/.doc/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)

## Contributing

Contributions are welcome! Please read the [Contributing Guidelines](/.doc/contributing.md) for more information.

## Testing

To run tests locally, use the following commands:

```sh
dotnet test tests/Ambev.DeveloperEvaluation.Unit
dotnet test tests/Ambev.DeveloperEvaluation.Integration
```

## GitHub Actions

To validate the project in GitHub Actions, open a Pull Request (PR) on GitHub. The pipeline will automatically run the tests and validate the project.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Additional Documentation

- [Overview](/.doc/overview.md)
- [Sales Requirement](/.doc/SALES_REQUERIMENT.md)
- [Tech Stack](/.doc/tech-stack.md)
- [Frameworks](/.doc/frameworks.md)
- [Project Structure](/.doc/project-structure.md)
- [API General](/.doc/general-api.md)
- [Products API](/.doc/products-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)

# DukandaCore

A clean architecture .NET solution for the Dukanda platform, built using the [Clean.Architecture.Solution.Template](https://github.com/jasontaylordev/CleanArchitecture).

## Prerequisites

- .NET 8.0 SDK or later
- SQL Server

## Getting Started

1. Clone the repository
2. Build the solution:
```bash
dotnet build -tl
```

## Development

To run the web application:

```bash
cd src/Web/
dotnet watch run
```

Navigate to https://localhost:5001. The application will automatically reload when you make changes to the source files.

## Project Structure

- `src/`
  - `Application/` - Application layer containing business logic, interfaces, and DTOs
  - `Domain/` - Domain entities and business rules
  - `Infrastructure/` - External concerns and implementations
  - `Web/` - API endpoints and web-specific concerns

## Testing

The solution includes different types of tests:

- Unit Tests
- Integration Tests
- Functional Tests
- Acceptance Tests

### Running Tests

To run all tests except acceptance tests:
```bash
dotnet test --filter "FullyQualifiedName!~AcceptanceTests"
```

For acceptance tests:
1. Start the application:
```bash
cd src/Web/
dotnet run
```

2. In a new terminal, run:
```bash
cd tests/AcceptanceTests/
dotnet test
```

## Code Style

This project uses [EditorConfig](https://editorconfig.org/) to maintain consistent coding styles. The rules are defined in the `.editorconfig` file at the solution root.

## Contributing

1. Create a new branch for your feature
2. Make your changes
3. Submit a pull request
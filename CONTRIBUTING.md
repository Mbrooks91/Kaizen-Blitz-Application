# Contributing to Kaizen Blitz Application

Thank you for considering contributing to the Kaizen Blitz Application! This document provides guidelines for contributing to this project.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/your-username/kaizen-blitz-windows.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Commit your changes: `git commit -am 'Add some feature'`
6. Push to the branch: `git push origin feature/your-feature-name`
7. Create a Pull Request

## Development Setup

### Prerequisites

- Windows 10 or later
- .NET 6.0 SDK
- Visual Studio 2022 or VS Code with C# extension
- Git

### Building the Project

```bash
cd src
dotnet restore
dotnet build KaizenBlitz.sln
```

### Running Tests

```bash
dotnet test KaizenBlitz.sln
```

## Code Style Guidelines

- Follow the `.editorconfig` settings
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and single-purpose
- Use async/await for I/O operations
- Handle exceptions appropriately

## Coding Conventions

- Use PascalCase for class names, method names, and properties
- Use camelCase for local variables and private fields
- Prefix interfaces with 'I' (e.g., `IProjectRepository`)
- Use nullable reference types where appropriate
- Follow SOLID principles

## Pull Request Process

1. Ensure your code builds without warnings
2. Update documentation if needed
3. Add tests for new functionality
4. Ensure all tests pass
5. Update the README.md if you add new features
6. Your PR will be reviewed by maintainers

## Reporting Bugs

When reporting bugs, please include:

- A clear and descriptive title
- Steps to reproduce the issue
- Expected behavior
- Actual behavior
- Screenshots if applicable
- Your environment (Windows version, .NET version)

## Feature Requests

We welcome feature requests! Please provide:

- A clear description of the feature
- Why this feature would be useful
- Any examples or mockups if applicable

## Questions?

Feel free to open an issue with your question or contact the maintainers.

Thank you for contributing!

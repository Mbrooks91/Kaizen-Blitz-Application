# Architecture Documentation

## Overview

The Kaizen Blitz Application follows a clean, layered architecture based on MVVM (Model-View-ViewModel) pattern and Domain-Driven Design principles.

## Architecture Layers

### 1. Core Layer (`KaizenBlitz.Core`)

**Purpose**: Contains domain models, enums, interfaces, and DTOs. This layer has no dependencies on other projects.

**Components**:
- **Models**: Domain entities representing business concepts
  - `Project`: Main entity for Kaizen projects
  - `FiveWhys`: Root cause analysis tool
  - `IshikawaDiagram`: Cause-and-effect analysis
  - `ActionPlan` & `ActionPlanTask`: Implementation planning
  - `ParetoChart` & `ParetoItem`: Statistical analysis

- **Enums**: Type-safe enumeration values
  - `ProjectStatus`: InProgress, Completed, OnHold, Cancelled
  - `KaizenPhase`: Preparation, Analysis, Improvement, Implementation, Review
  - `TaskStatus`: NotStarted, InProgress, Completed, Blocked, Cancelled

- **Interfaces**: Contracts for services and repositories
  - `IProjectRepository`: Project data access
  - `IToolRepository`: Tool data access
  - `IPdfService`: PDF generation
  - `IExportService`: Document export
  - `IEmailService`: Email operations

- **DTOs**: Data transfer objects for API/UI communication

### 2. Data Layer (`KaizenBlitz.Data`)

**Purpose**: Handles data persistence using Entity Framework Core with SQLite.

**Components**:
- **ApplicationDbContext**: EF Core database context
  - Configures entity relationships
  - Handles JSON serialization for complex types
  - Manages database migrations

- **Repositories**: Implementation of data access patterns
  - `ProjectRepository`: CRUD operations for projects
  - `ToolRepository`: CRUD operations for quality tools
  - Uses async/await for all operations
  - Includes related entities using `.Include()`

**Database Schema**:
```
Projects (1) ─┬─ (1) FiveWhys
              ├─ (1) IshikawaDiagram ─── (n) IshikawaCategories
              ├─ (1) ActionPlan ──────── (n) ActionPlanTasks
              └─ (1) ParetoChart ──────── (n) ParetoItems
```

### 3. Services Layer (`KaizenBlitz.Services`)

**Purpose**: Business logic and external integrations.

**Components**:
- **PdfService**: Generates PDF documents using QuestPDF
  - Project summaries
  - Tool-specific reports
  - Professional formatting with headers/footers

- **WordExportService**: Creates Word documents using OpenXML SDK
  - Editable project documentation
  - Structured with headings and tables

- **ExcelExportService**: Exports to Excel using EPPlus
  - Action plans with formatting
  - Pareto data with embedded charts

- **EmailService**: Opens default email client with attachments

- **ZipService**: Creates compressed archives of project files

### 4. WPF Layer (`KaizenBlitz.WPF`)

**Purpose**: User interface and presentation logic.

**MVVM Pattern**:

```
View (XAML) ←→ ViewModel ←→ Model (Core)
                    ↓
              Services/Repositories
```

**Components**:

- **Views**: XAML-based user interface
  - `MainWindow`: Application shell with navigation
  - `ProjectDashboardView`: Project listing and management
  - `ProjectWizardView`: Multi-step project creation
  - `FiveWhysView`: Root cause analysis interface
  - `IshikawaView`: Fishbone diagram editor
  - `ActionPlanView`: Task management grid

- **ViewModels**: Presentation logic and data binding
  - Inherit from `ViewModelBase` (ObservableObject)
  - Use `RelayCommand` for command binding
  - Implement async operations with proper error handling
  - Manage UI state (IsBusy, ErrorMessage)

- **Helpers**:
  - `NavigationService`: View navigation
  - `RelayCommand`: ICommand implementation
  - **Converters**: Value conversion for data binding
    - `BoolToVisibilityConverter`
    - `StatusToColorConverter`
    - `ProgressToPercentageConverter`

- **Resources**:
  - Color schemes and branding
  - Button and control styles
  - Material Design integration

### 5. Tests Layer (`KaizenBlitz.Tests`)

**Purpose**: Unit and integration testing.

**Components**:
- Service tests using xUnit
- Repository tests with in-memory database
- Mocking using Moq framework

## Design Patterns

### 1. MVVM (Model-View-ViewModel)
- **Separation of concerns**: UI decoupled from business logic
- **Data binding**: Automatic UI updates via INotifyPropertyChanged
- **Commands**: User actions handled through ICommand

### 2. Repository Pattern
- **Abstraction**: Data access abstracted behind interfaces
- **Testability**: Easy to mock for unit testing
- **Flexibility**: Can swap data sources without changing business logic

### 3. Dependency Injection
- **Loose coupling**: Dependencies injected via constructor
- **Lifetime management**: Configured in `App.xaml.cs`
  - Singleton: Configuration, NavigationService
  - Scoped: DbContext, Repositories, Services
  - Transient: ViewModels, Views

### 4. Async/Await Pattern
- All I/O operations are asynchronous
- Prevents UI freezing
- Proper exception handling with try-catch

## Data Flow

### Creating a Project
```
User Input (View)
    ↓
Command Binding
    ↓
ViewModel (validation)
    ↓
Repository (data access)
    ↓
DbContext (EF Core)
    ↓
SQLite Database
    ↓
Return Result
    ↓
Update ViewModel Properties
    ↓
UI Updates (data binding)
```

### Exporting PDF
```
User Click Export Button
    ↓
ViewModel.ExportPdfCommand
    ↓
IPdfService.GeneratePdfAsync()
    ↓
QuestPDF Document Creation
    ↓
Return byte[]
    ↓
SaveFileDialog
    ↓
File.WriteAllBytesAsync()
```

## Technology Choices

### SQLite
- **Rationale**: Lightweight, file-based, no server required
- **Benefits**: Easy deployment, xcopy deployment, good for desktop apps

### Entity Framework Core
- **Rationale**: Industry-standard ORM with excellent tooling
- **Benefits**: Migrations, LINQ queries, change tracking

### QuestPDF
- **Rationale**: Modern, fluent API for PDF generation
- **Benefits**: Great documentation, easy to use, fast

### Material Design
- **Rationale**: Modern, consistent UI framework
- **Benefits**: Professional appearance, rich controls

## Extensibility Points

### Adding New Tools
1. Create model in Core layer
2. Add DbSet in ApplicationDbContext
3. Create migration
4. Implement repository methods
5. Create ViewModel
6. Create View (XAML)
7. Register in DI container

### Adding New Export Formats
1. Create interface in Core (e.g., `IJsonExportService`)
2. Implement service in Services layer
3. Inject into ViewModels
4. Add export button to UI

### Custom Themes
1. Create new ResourceDictionary in Resources/Styles
2. Define color schemes
3. Merge in App.xaml
4. Configure via appsettings.json

## Performance Considerations

- **Lazy loading**: Related entities loaded only when needed
- **Async operations**: Prevents UI blocking
- **Data pagination**: For large project lists (future enhancement)
- **Caching**: ViewModels cache loaded data

## Security Considerations

- **SQL Injection**: Protected by EF Core parameterized queries
- **File paths**: Validated before file operations
- **Input validation**: Required fields enforced in ViewModels and Models

## Future Enhancements

- Cloud synchronization (Azure Blob Storage)
- Real-time collaboration (SignalR)
- Advanced reporting (Power BI integration)
- Mobile companion app (Xamarin/MAUI)

# Development Setup Guide

This guide will help you set up the Event Management System for local development.

## Prerequisites

### Required Tools
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli): `npm install -g @angular/cli`
- [Git](https://git-scm.com/)

### Optional Tools
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local) (for advanced scenarios)
- [Docker](https://www.docker.com/get-started) (for containerized development)
- [Visual Studio Code](https://code.visualstudio.com/) with recommended extensions

## Initial Setup

### 1. Clone the Repository
```bash
git clone https://github.com/wullemsb/codingagentdemo.git
cd codingagentdemo
```

### 2. Backend Setup

Navigate to the backend directory:
```bash
cd backend
```

Install dependencies:
```bash
dotnet restore
```

Build the project:
```bash
dotnet build
```

Run the backend:
```bash
dotnet run
```

The backend will start on `http://localhost:7071`

### 3. Frontend Setup

Open a new terminal and navigate to the frontend directory:
```bash
cd frontend
```

Install dependencies:
```bash
npm install
```

Start the development server:
```bash
npm start
```

The frontend will start on `http://localhost:4200`

## Development Workflow

### Backend Development

#### Running the Backend
```bash
cd backend
dotnet run
```

#### Building the Backend
```bash
cd backend
dotnet build
```

#### Running Tests
```bash
cd backend
dotnet test
```

#### Adding New Dependencies
```bash
cd backend
dotnet add package <PackageName>
```

#### Database Migrations (when using real database)
```bash
cd backend
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Frontend Development

#### Starting Development Server
```bash
cd frontend
npm start
```

#### Building for Production
```bash
cd frontend
npm run build
```

#### Running Tests
```bash
cd frontend
npm test
```

#### Running Linter
```bash
cd frontend
npm run lint
```

#### Adding New Dependencies
```bash
cd frontend
npm install <package-name>
```

#### Generating Components
```bash
cd frontend
ng generate component components/my-component
ng generate service services/my-service
```

## Configuration

### Backend Configuration

The backend uses these configuration files:
- `appsettings.json` - Application settings
- `local.settings.json` - Local development settings
- `host.json` - Azure Functions host configuration

### Frontend Configuration

The frontend configuration is in:
- `src/environments/environment.ts` - Development environment
- `src/environments/environment.prod.ts` - Production environment
- `angular.json` - Angular CLI configuration

## API Development

### Backend API Structure

```
backend/
├── Functions/
│   ├── EventsFunctions.cs          # Event CRUD operations
│   ├── EventRegistrationsFunctions.cs  # Registration operations
│   └── CorsFunction.cs             # CORS preflight handling
├── Models/
│   ├── Event.cs                    # Event model
│   └── EventRegistration.cs       # Registration model
├── Data/
│   └── EventsDbContext.cs          # Entity Framework context
└── Helpers/
    └── CorsHelper.cs               # CORS utilities
```

### Adding New API Endpoints

1. Create a new function in the appropriate Functions class:
```csharp
[Function("MyNewFunction")]
public async Task<IActionResult> MyNewFunction(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "my-endpoint")] HttpRequest req)
{
    // Implementation
}
```

2. Add CORS support using CorsHelper:
```csharp
return CorsHelper.CreateOkResponseWithCors(data, req);
```

### Frontend Service Development

1. Generate a new service:
```bash
ng generate service services/my-service
```

2. Implement HTTP methods:
```typescript
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MyService {
  private baseUrl = 'http://localhost:7071/api';
  
  constructor(private http: HttpClient) { }
  
  getData(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/my-endpoint`);
  }
}
```

## Testing

### Backend Testing

The project uses xUnit for testing. Create test files in the test project:

```csharp
public class EventsFunctionsTests
{
    [Fact]
    public async Task GetEvents_ReturnsAllEvents()
    {
        // Arrange
        // Act
        // Assert
    }
}
```

### Frontend Testing

The project uses Jasmine and Karma for testing:

```typescript
describe('EventsService', () => {
  let service: EventsService;
  
  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventsService);
  });
  
  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
```

## Debugging

### Backend Debugging

1. **Visual Studio Code**: Use the built-in debugger with F5
2. **Visual Studio**: Open the solution and press F5
3. **Command Line**: Use `dotnet run` and attach a debugger

### Frontend Debugging

1. **Browser DevTools**: Use Chrome/Edge DevTools for debugging
2. **Visual Studio Code**: Install Angular extension pack
3. **Angular DevTools**: Browser extension for Angular-specific debugging

## Common Issues and Solutions

### Backend Issues

**Issue**: CORS errors when calling API from frontend
```
Solution: Ensure CORS is properly configured in the backend and headers are added to responses
```

**Issue**: Entity Framework migration errors
```
Solution: Ensure database connection string is correct and migrations are applied
```

### Frontend Issues

**Issue**: Module not found errors
```
Solution: Run `npm install` to ensure all dependencies are installed
```

**Issue**: Angular compilation errors
```
Solution: Check TypeScript version compatibility and ensure all imports are correct
```

## Performance Optimization

### Backend Performance
- Use async/await for all database operations
- Implement proper caching strategies
- Use appropriate HTTP status codes
- Implement request/response compression

### Frontend Performance
- Use OnPush change detection strategy
- Implement lazy loading for routes
- Optimize bundle size with tree shaking
- Use Angular's built-in performance tools

## Security Considerations

### Backend Security
- Validate all input parameters
- Implement proper error handling
- Use parameterized queries
- Keep dependencies updated

### Frontend Security
- Sanitize user inputs
- Implement proper form validation
- Use HTTPS in production
- Keep dependencies updated

## Deployment Preparation

### Development to Production Checklist

- [ ] Update environment configurations
- [ ] Run all tests
- [ ] Build production builds
- [ ] Update database connection strings
- [ ] Configure CORS for production domains
- [ ] Review security settings
- [ ] Test Docker containers
- [ ] Update documentation

## Getting Help

If you encounter issues:

1. Check this documentation
2. Review the [README.md](../README.md)
3. Search existing GitHub issues
4. Create a new issue with detailed information

## VS Code Extensions

Recommended extensions for development:

### Backend (.NET)
- C# Dev Kit
- .NET Runtime and SDK
- Azure Functions

### Frontend (Angular)
- Angular Language Service
- TypeScript Importer
- Prettier - Code formatter
- ESLint

### General
- GitLens
- Live Share
- Docker
- Thunder Client (for API testing)
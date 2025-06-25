# Event Management System

A full-stack event management application built with Angular frontend and Azure Functions backend.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Getting Started](#getting-started)
- [Development](#development)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## 🌟 Overview

This project is a comprehensive event management system that allows users to:
- Browse and filter events
- View detailed event information
- Register for events
- Manage event data (CRUD operations)

## 🏗️ Architecture

The application follows a clean architecture pattern with:

### Backend (Azure Functions)
- **Language**: C# with .NET 8
- **Framework**: Azure Functions v4 with isolated worker
- **Database**: In-memory Entity Framework Core (development)
- **API**: RESTful APIs with CORS support

### Frontend (Angular)
- **Language**: TypeScript
- **Framework**: Angular 17+ with standalone components
- **Styling**: CSS with responsive design
- **HTTP Client**: Angular HttpClient with error handling

### Data Contract
- Shared models between frontend and backend
- Consistent data structure across the application

## ✨ Features

### Backend APIs
- `GET /api/events` - Get all events with optional filtering
- `GET /api/events/{id}` - Get specific event
- `POST /api/events` - Create new event
- `PUT /api/events/{id}` - Update existing event
- `DELETE /api/events/{id}` - Delete event
- `POST /api/events/{id}/register` - Register for event
- `GET /api/events/{id}/registrations` - Get event registrations

### Frontend Components
- **Event List**: Browse and filter events
- **Event Details**: View detailed event information
- **Event Registration**: Register for events with form validation
- **Error Handling**: User-friendly error messages

### Event Data Model
- Name (required)
- Location (required)
- Date (required)
- Start Time (required)
- Created/Updated timestamps

### Registration Data Model
- Name (required)
- Email (required)
- Pronouns (optional)
- Communication opt-in (boolean)

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/get-started) (optional)

### Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/wullemsb/codingagentdemo.git
   cd codingagentdemo
   ```

2. **Start the backend**
   ```bash
   cd backend
   dotnet restore
   dotnet run
   ```
   Backend will be available at `http://localhost:7071`

3. **Start the frontend** (in a new terminal)
   ```bash
   cd frontend
   npm install
   npm start
   ```
   Frontend will be available at `http://localhost:4200`

4. **Open your browser** and navigate to `http://localhost:4200`

## 💻 Development

### Local Development Setup

See [docs/development.md](docs/development.md) for detailed development setup instructions.

### GitHub Codespaces

This project is configured for GitHub Codespaces. See [docs/codespaces.md](docs/codespaces.md) for setup instructions.

### Project Structure

```
.
├── backend/                 # Azure Functions C# backend
│   ├── Data/               # Entity Framework DbContext
│   ├── Functions/          # Azure Functions endpoints
│   ├── Models/             # Data models
│   ├── Helpers/            # Utility classes
│   └── Dockerfile          # Backend container configuration
├── frontend/               # Angular frontend application
│   ├── src/app/
│   │   ├── components/     # Angular components
│   │   ├── models/         # TypeScript models
│   │   └── services/       # Angular services
│   ├── Dockerfile          # Frontend container configuration
│   └── nginx.conf          # Nginx configuration
├── datacontract/           # Shared data models
├── docs/                   # Documentation
└── .github/                # GitHub configuration
    ├── workflows/          # CI/CD pipelines
    ├── dependabot.yml      # Dependency management
    └── SECURITY.md         # Security policy
```

## 🐳 Deployment

### Docker

Both backend and frontend include Dockerfiles for containerized deployment:

```bash
# Build backend container
cd backend
docker build -t event-backend .

# Build frontend container
cd frontend
docker build -t event-frontend .
```

### CI/CD

The project includes GitHub Actions workflows for:
- Automated testing and building
- Docker image creation
- Dependency scanning with Dependabot
- Security scanning

## 🔧 Configuration

### Backend Configuration

The backend uses the following configuration:
- **Development**: In-memory database
- **CORS**: Configured for `http://localhost:4200`
- **Logging**: Console and Application Insights

### Frontend Configuration

The frontend is configured to:
- Connect to backend at `http://localhost:7071/api`
- Use Angular's HttpClient with error handling
- Implement responsive design

## 🧪 Testing

### Backend Tests
```bash
cd backend
dotnet test
```

### Frontend Tests
```bash
cd frontend
npm test
```

## 📚 API Documentation

### Events API

#### Get Events
```http
GET /api/events?dateFrom=2024-01-01&dateTo=2024-12-31&location=Seattle
```

#### Create Event
```http
POST /api/events
Content-Type: application/json

{
  "name": "Tech Conference 2024",
  "location": "Seattle, WA",
  "date": "2024-06-15T00:00:00Z",
  "startTime": "09:00:00"
}
```

See [docs/api.md](docs/api.md) for complete API documentation.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request

Please read our [Contributing Guidelines](docs/contributing.md) for more details.

## 🔒 Security

This project follows security best practices. Please review our [Security Policy](.github/SECURITY.md) for reporting vulnerabilities.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙋‍♂️ Support

If you have questions or need help:
- Create an issue on GitHub
- Check the [documentation](docs/)
- Review the [FAQ](docs/faq.md)

---

**Made with ❤️ by the Event Management Team**
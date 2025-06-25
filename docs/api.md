# API Documentation

## Base URL
- **Development**: `http://localhost:7071/api`
- **Production**: `https://your-functions-app.azurewebsites.net/api`

## Authentication
All endpoints use `AuthorizationLevel.Anonymous` for development. In production, implement appropriate authentication.

## Events API

### Get All Events
```http
GET /api/events
```

**Query Parameters:**
- `dateFrom` (optional): Filter events from this date (ISO 8601 format)
- `dateTo` (optional): Filter events to this date (ISO 8601 format)  
- `location` (optional): Filter events by location (partial match)

**Response:**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "Tech Conference 2024",
    "location": "Seattle, WA",
    "date": "2024-06-15T00:00:00Z",
    "startTime": "09:00:00",
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  }
]
```

### Get Specific Event
```http
GET /api/events/{id}
```

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Tech Conference 2024",
  "location": "Seattle, WA",
  "date": "2024-06-15T00:00:00Z",
  "startTime": "09:00:00",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### Create Event
```http
POST /api/events
Content-Type: application/json
```

**Request Body:**
```json
{
  "name": "Tech Conference 2024",
  "location": "Seattle, WA",
  "date": "2024-06-15T00:00:00Z",
  "startTime": "09:00:00"
}
```

**Response:** `201 Created`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Tech Conference 2024",
  "location": "Seattle, WA",
  "date": "2024-06-15T00:00:00Z",
  "startTime": "09:00:00",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### Update Event
```http
PUT /api/events/{id}
Content-Type: application/json
```

**Request Body:**
```json
{
  "name": "Updated Tech Conference 2024",
  "location": "Portland, OR",
  "date": "2024-06-16T00:00:00Z",
  "startTime": "10:00:00"
}
```

**Response:** `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Updated Tech Conference 2024",
  "location": "Portland, OR",
  "date": "2024-06-16T00:00:00Z",
  "startTime": "10:00:00",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T12:45:00Z"
}
```

### Delete Event
```http
DELETE /api/events/{id}
```

**Response:** `204 No Content`

## Event Registration API

### Register for Event
```http
POST /api/events/{eventId}/register
Content-Type: application/json
```

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "pronouns": "he/him",
  "optInForCommunication": true
}
```

**Response:** `201 Created`
```json
{
  "id": "7fa85f64-5717-4562-b3fc-2c963f66afa7",
  "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "John Doe",
  "email": "john.doe@example.com",
  "pronouns": "he/him",
  "optInForCommunication": true,
  "createdAt": "2024-01-15T11:00:00Z"
}
```

### Get Event Registrations
```http
GET /api/events/{eventId}/registrations
```

**Response:**
```json
[
  {
    "id": "7fa85f64-5717-4562-b3fc-2c963f66afa7",
    "eventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "pronouns": "he/him",
    "optInForCommunication": true,
    "createdAt": "2024-01-15T11:00:00Z"
  }
]
```

## Error Responses

### 400 Bad Request
```json
{
  "error": "Invalid event data"
}
```

### 404 Not Found
```json
{
  "error": "Event not found"
}
```

### 409 Conflict
```json
{
  "error": "User already registered for this event"
}
```

### 500 Internal Server Error
```json
{
  "error": "An internal server error occurred"
}
```

## CORS Configuration

The API is configured with CORS headers for development:
- **Allowed Origins**: `http://localhost:4200`
- **Allowed Methods**: `GET, POST, PUT, DELETE, OPTIONS`
- **Allowed Headers**: `Content-Type, Authorization`
- **Credentials**: Allowed

## Data Models

### Event
```typescript
interface Event {
  id: string;                 // GUID
  name: string;              // Required, max 200 chars
  location: string;          // Required, max 500 chars
  date: string;              // ISO 8601 date
  startTime: string;         // Time format HH:mm
  createdAt: string;         // ISO 8601 datetime
  updatedAt: string;         // ISO 8601 datetime
}
```

### Event Registration
```typescript
interface EventRegistration {
  id: string;                     // GUID
  eventId: string;               // GUID, foreign key
  name: string;                  // Required, max 100 chars
  email: string;                 // Required, valid email, max 200 chars
  pronouns?: string;             // Optional, max 100 chars
  optInForCommunication: boolean; // Required
  createdAt: string;             // ISO 8601 datetime
}
```

### Event Filter
```typescript
interface EventsFilter {
  dateFrom?: string;    // ISO 8601 date
  dateTo?: string;      // ISO 8601 date
  location?: string;    // Partial match string
}
```

## Testing the API

### Using curl

**Get all events:**
```bash
curl -X GET "http://localhost:7071/api/events"
```

**Create an event:**
```bash
curl -X POST "http://localhost:7071/api/events" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Event",
    "location": "Test Location",
    "date": "2024-12-25T00:00:00Z",
    "startTime": "14:00:00"
  }'
```

**Register for an event:**
```bash
curl -X POST "http://localhost:7071/api/events/{eventId}/register" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test User",
    "email": "test@example.com",
    "pronouns": "they/them",
    "optInForCommunication": false
  }'
```

### Using Thunder Client (VS Code Extension)

1. Install Thunder Client extension
2. Create new requests with the endpoints above
3. Set headers and request body as needed
4. Test all endpoints

## Database Schema

### Events Table
```sql
CREATE TABLE Events (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Location NVARCHAR(500) NOT NULL,
    Date DATETIME2 NOT NULL,
    StartTime TIME NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);
```

### EventRegistrations Table
```sql
CREATE TABLE EventRegistrations (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    EventId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Pronouns NVARCHAR(100) NULL,
    OptInForCommunication BIT NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE
);
```

## Rate Limiting

In production, consider implementing rate limiting:
- Events API: 100 requests per minute per IP
- Registration API: 10 registrations per minute per IP

## Security Considerations

1. **Input Validation**: All inputs are validated on the server
2. **SQL Injection**: Using Entity Framework prevents SQL injection
3. **Cross-Site Scripting**: Inputs are not rendered as HTML
4. **Authentication**: Implement proper authentication for production
5. **Authorization**: Add role-based access control as needed
6. **HTTPS**: Use HTTPS in production environments
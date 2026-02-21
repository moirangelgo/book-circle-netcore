# 📚 BookCircle API

A RESTful API for **BookCircle** — a collaborative book club platform. Built with **.NET 8** following **Clean Architecture** principles.

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 8 Web API |
| Language | C# 12 (Primary Constructors) |
| ORM | Entity Framework Core 8 |
| Database | SQL Server / LocalDB |
| Auth | JWT Bearer (OAuth2 Password Flow) |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| Docs | Swagger / OpenAPI |

---

## 🏗️ Architecture

Clean Architecture with 4 layers:

```
src/
├── BookCircle.Domain/          # Entities, Enums, Repository interfaces
├── BookCircle.Application/     # DTOs, Validators, AutoMapper, Services
├── BookCircle.Infrastructure/  # EF Core DbContext, Repositories, DI
└── BookCircle.API/             # Controllers, Middleware, Program.cs
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server **or** LocalDB (included with Visual Studio)

### 1. Clone & Configure

Update the connection string in `src/BookCircle.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookCircleDb;Trusted_Connection=True;"
}
```

> ⚠️ **Change the JWT secret** before any production deployment:
> ```json
> "JwtSettings": { "SecretKey": "Your-256-bit-secret-here" }
> ```

### 2. Apply Migrations

```powershell
dotnet ef migrations add InitialCreate `
  --project src\BookCircle.Infrastructure `
  --startup-project src\BookCircle.API

dotnet ef database update `
  --project src\BookCircle.Infrastructure `
  --startup-project src\BookCircle.API
```

### 3. Run the API

```powershell
dotnet run --project src\BookCircle.API
```

Swagger UI → **[http://localhost:5054/swagger](http://localhost:5054/swagger)**

---

## 🔐 Authentication

The API uses **JWT Bearer** tokens (OAuth2 Password Flow).

**1. Register a user:**
```http
POST /auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "username": "johndoe",
  "password": "secret123",
  "fullName": "John Doe"
}
```

**2. Obtain a token:**
```http
POST /token
Content-Type: application/x-www-form-urlencoded

username=johndoe&password=secret123
```

**3. Use the token** in all subsequent requests:
```http
Authorization: Bearer <access_token>
```

---

## 📋 Endpoints

### Auth
| Method | Route | Auth | Description |
|--------|-------|:----:|-------------|
| `GET` | `/health` | | Health check |
| `POST` | `/token` | | Get JWT token |
| `POST` | `/auth/register` | | Register user |

### Clubs
| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/clubs` | List clubs (`?skip=0&limit=100`) |
| `POST` | `/clubs` | Create club |
| `GET` | `/clubs/{id}` | Get club |
| `PUT` | `/clubs/{id}` | Update club |
| `DELETE` | `/clubs/{id}` | Delete club |

### Books
| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/clubs/{id}/books` | List books |
| `POST` | `/clubs/{id}/books` | Add book |
| `GET` | `/clubs/{id}/books/{bookId}` | Get book |
| `GET` | `/clubs/{id}/books/{bookId}/votes` | Get votes |
| `DELETE` | `/clubs/{id}/books/{bookId}/votes` | Reset votes |
| `GET` | `/clubs/{clubId}/books/{bookId}/progress` | Get progress |
| `PUT` | `/clubs/{clubId}/books/{bookId}/progress` | Update progress |

### Reviews
| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/clubs/{id}/books/{bookId}/reviews` | List reviews |
| `POST` | `/clubs/{id}/books/{bookId}/reviews` | Create review |
| `PUT` | `/clubs/{id}/books/{bookId}/reviews/{reviewId}` | Update review |
| `DELETE` | `/clubs/{id}/books/{bookId}/reviews/{reviewId}` | Delete review |

### Meetings
| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/clubs/{id}/meetings` | List meetings |
| `POST` | `/clubs/{id}/meetings` | Schedule meeting |
| `GET` | `/clubs/{id}/meetings/{meetingId}` | Get meeting |
| `DELETE` | `/clubs/{id}/meetings/{meetingId}` | Cancel meeting |
| `POST` | `/clubs/{id}/meetings/{meetingId}/attendance` | Confirm attendance (`SI`, `NO`, `TAL_VEZ`) |

---

## 🗄️ Domain Model

```
User ──────────────────────────────────────────┐
Club ──< Book ──< Review                       │
     └──< Meeting ──< MeetingAttendance >── User
```

---

## 📁 Project Structure

```
book-circle-netcore/
├── BookCircle.slnx
├── books.api.json              # Original OpenAPI spec
├── README.md
└── src/
    ├── BookCircle.Domain/
    │   ├── Entities/           # User, Club, Book, Review, Meeting, MeetingAttendance
    │   ├── Enums/              # AttendanceValue (SI, NO, TAL_VEZ)
    │   └── Interfaces/         # Repository contracts
    ├── BookCircle.Application/
    │   ├── DTOs/               # Request/Response records
    │   ├── Validators/         # FluentValidation rules
    │   ├── Mappings/           # AutoMapper profile
    │   └── Services/           # Business logic (Auth, Club, Book, Review, Meeting)
    ├── BookCircle.Infrastructure/
    │   ├── Data/               # AppDbContext
    │   ├── Configurations/     # EF Core Fluent API configs
    │   ├── Repositories/       # EF Core implementations
    │   └── DependencyInjection.cs
    └── BookCircle.API/
        ├── Controllers/        # 6 controllers, 24 endpoints
        ├── Middleware/         # Global exception handler (RFC 7807)
        ├── Program.cs
        └── appsettings.json
```

---

## 🧱 Build

```powershell
dotnet build BookCircle.slnx
```

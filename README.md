# StudyTool

A full-stack interview preparation and study card application built with Blazor Server, 
ASP.NET Core Minimal API, Entity Framework Core, and Azure OpenAI.

## Architecture
```
StudyTool.sln
├── StudyTool.Web       # Blazor Server UI (cookie authentication)
├── StudyTool.Api       # ASP.NET Core Minimal API (JWT authentication)
├── StudyTool.Core      # Domain models, interfaces, business logic
├── StudyTool.Data      # EF Core DbContext, migrations, repositories
└── StudyTool.Tests     # xUnit unit tests
```

## Tech Stack

| Area | Technology |
|---|---|
| Framework | .NET 10 |
| UI | Blazor Server |
| API | ASP.NET Core Minimal API |
| ORM | Entity Framework Core |
| Database | Azure SQL |
| AI | Azure OpenAI (gpt-4o-mini) / Ollama (local dev) |
| CSS | Tailwind CSS v4 |
| Testing | xUnit, Moq |

## Getting Started

### Prerequisites
- .NET 10 SDK
- Node.js 18+
- SQL Server or Azure SQL access
- Ollama (local AI) or Azure OpenAI credentials

### Setup

**1 — Clone the repo**
```bash
git clone https://github.com/yourusername/StudyTool.git
cd StudyTool
```

**2 — Install Node dependencies**
```bash
cd StudyTool.Web
npm install
cd ..
```

**3 — Configure user secrets**

For `StudyTool.Web`:
```bash
dotnet user-secrets init --project StudyTool.Web
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>" --project StudyTool.Web
dotnet user-secrets set "Auth:Users:0:Id" "owner" --project StudyTool.Web
dotnet user-secrets set "Auth:Users:0:Username" "admin" --project StudyTool.Web
dotnet user-secrets set "Auth:Users:0:Password" "<your-password>" --project StudyTool.Web
dotnet user-secrets set "Auth:Users:1:Id" "demo" --project StudyTool.Web
dotnet user-secrets set "Auth:Users:1:Username" "demo" --project StudyTool.Web
dotnet user-secrets set "Auth:Users:1:Password" "<your-password>" --project StudyTool.Web
```

For `StudyTool.Api`:
```bash
dotnet user-secrets init --project StudyTool.Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>" --project StudyTool.Api
dotnet user-secrets set "Auth:Users:0:Id" "owner" --project StudyTool.Api
dotnet user-secrets set "Auth:Users:0:Username" "admin" --project StudyTool.Api
dotnet user-secrets set "Auth:Users:0:Password" "<your-password>" --project StudyTool.Api
dotnet user-secrets set "Auth:Users:1:Id" "demo" --project StudyTool.Api
dotnet user-secrets set "Auth:Users:1:Username" "demo" --project StudyTool.Api
dotnet user-secrets set "Auth:Users:1:Password" "<your-password>" --project StudyTool.Api
```

For Azure OpenAI (when quota approved):
```bash
dotnet user-secrets set "AzureOpenAI:Endpoint" "<your-endpoint>" --project StudyTool.Web
dotnet user-secrets set "AzureOpenAI:Key" "<your-key>" --project StudyTool.Web
dotnet user-secrets set "AzureOpenAI:DeploymentName" "<your-deploymentname>" --project StudyTool.Web
```

**4 — Apply database migrations**
```bash
dotnet ef database update --project StudyTool.Data --startup-project StudyTool.Web
```

**5 — Run the application**

Set both `StudyTool.Web` and `StudyTool.Api` as startup projects in Visual Studio, then press F5.

- Web UI: `https://localhost:7077`
- API: `https://localhost:7200`
- API health check: `https://localhost:7200/api/health`

## Known Simplifications

| Area | Current approach | Real-world approach |
|---|---|---|
| Authentication | Hardcoded users in user-secrets | Identity Provider (Azure AD, Auth0) |
| User management | String user IDs, no user store | Proper user database with roles |
| Secrets management | dotnet user-secrets (local) | Azure Key Vault with Managed Identity |
| AI service | Ollama local model (dev) | Azure OpenAI gpt-4o-mini (pending quota) |
| API documentation | .NET 10 OpenAPI / Scalar UI | Would add versioning in production |

## Secret Management

Secrets are managed using `dotnet user-secrets` for local development. 
These are stored outside the project directory at:
```
Windows: %APPDATA%\Microsoft\UserSecrets\<guid>\secrets.json
```

They are never committed to source control. In a real-world team environment 
secrets would be shared via a secrets management platform such as HashiCorp Vault 
or Azure Key Vault, with production secrets injected via environment variables 
through Azure App Service Application Settings.

## Planned Features
- [ ] Card library with search and filtering
- [ ] Cue card quiz mode
- [ ] AI-powered multiple choice quiz (Q&A mode)
- [ ] REST API with JWT authentication
- [ ] Event streaming (Kafka)
- [ ] Message queuing (RabbitMQ)
- [ ] Azure deployment
```
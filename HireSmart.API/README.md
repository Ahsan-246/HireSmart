# HireSmart API

HireSmart API is the backend of a full-stack recruitment management system developed using ASP.NET Core Web API.

The system provides APIs for Candidates, Recruiters, and Administrators to manage jobs, applications, companies, resumes, authentication, and AI-based candidate evaluation.

---

## Technologies Used

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-Based Authorization
- Swagger / OpenAPI
- PdfPig
- Ollama
- Qwen 2.5:3B

---

## Main Features

### Authentication

- User registration
- User login
- JWT authentication
- Role-based authorization
- Candidate, Recruiter, and Admin roles

### Candidate

- Browse jobs
- Search jobs
- View job details
- Apply for jobs
- Upload resumes
- View resumes
- Delete resumes
- View submitted applications
- Delete applications
- Track application status

### Recruiter

- Create job postings
- Update job postings
- Delete job postings
- View candidate applications
- View candidate resumes
- Update application status
- Evaluate candidates using AI

### Administrator

- Manage users
- Manage companies
- Manage jobs
- View and manage system data

### AI Candidate Evaluation

The system includes an AI-based candidate evaluation module.

The AI evaluation process:

1. Retrieves the candidate's application.
2. Retrieves the candidate's job information.
3. Reads the candidate's uploaded PDF resume.
4. Extracts text from the PDF using PdfPig.
5. Sends the job description and resume information to the local AI model.
6. Uses Qwen 2.5:3B through Ollama.
7. Generates:
   - Match Score
   - Missing Skills
   - Candidate Summary
   - Recommendation
8. Stores the evaluation result in the database.

The evaluation is based on the skills and requirements explicitly provided in the job description.

---

## AI Evaluation

The AI evaluation module uses:

- Ollama
- Qwen 2.5:3B
- PdfPig

### Ollama Setup

Install Ollama on the local machine and download the required model:

```text
ollama pull qwen2.5:3b
```

Make sure Ollama is running before using the AI evaluation feature.

The Qwen model runs locally and is not included in this GitHub repository.

---

## Backend Setup

### 1. Clone the Repository

Clone this backend repository:

```text
git clone https://github.com/Ahsan-246/HireSmart
```

### 2. Open the Project

Open the `HireSmart.slnx` solution in Visual Studio.

Alternatively, open the `HireSmart.API` project directory using your preferred .NET development environment.

### 3. Configure SQL Server

Update the database connection string in:

```text
HireSmart.API/appsettings.json
```

Use your local SQL Server configuration.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
}
```

Do not commit passwords or other sensitive database credentials to GitHub.

### 4. Apply Database Migrations

Make sure the required Entity Framework Core tools are available, then run:

```text
dotnet ef database update
```

This creates/updates the required database using the existing migrations.

### 5. Run the API

Run the project from Visual Studio or use:

```text
dotnet run
```

Swagger/OpenAPI will be available at the URL shown by the application when it starts.

---

## API Modules

The backend provides APIs for:

- Authentication
- Users
- Companies
- Jobs
- Applications
- Resumes
- AI Evaluations

---

## Resume Handling

HireSmart supports resume management through the API, including:

- Resume upload
- Resume viewing
- Resume deletion
- Resume retrieval for candidate applications

Uploaded resume files are stored locally according to the application's configured file-storage implementation.

---

## Authentication and Authorization

The API uses JWT authentication and role-based authorization.

Available roles include:

- Candidate
- Recruiter
- Admin

Protected endpoints require a valid JWT authentication token and the appropriate role.

---

## Testing

API endpoints can be tested using:

- Swagger
- Postman

The main functionality can be tested through:

- User registration and login
- Authentication and authorization
- Job CRUD operations
- Job searching
- Application creation
- Application management
- Resume upload and retrieval
- Application status management
- AI candidate evaluation

---

## Project Structure

```text
HireSmart.API
│
├── Controllers
│
├── Data
│
├── DTOs
│
├── Models
│   └── Entities
│
├── Services
│   ├── Interfaces
│   └── Implementation
│
├── Migrations
│
├── Program.cs
├── appsettings.json
└── HireSmart.API.csproj
```

---

## AI Evaluation Flow

```text
Candidate Application
        │
        ▼
Candidate Resume
        │
        ▼
PDF Text Extraction
        │
        ▼
Job Description + Resume Text
        │
        ▼
Ollama
        │
        ▼
Qwen 2.5:3B
        │
        ▼
AI Evaluation
        │
        ├── Match Score
        ├── Missing Skills
        ├── Summary
        └── Recommendation
        │
        ▼
Database
```

---

## Important Notes

- SQL Server must be available locally.
- The database connection string must be configured before running the API.
- Database migrations should be applied before using the application.
- Ollama must be installed to use AI evaluation.
- The `qwen2.5:3b` model must be downloaded before using AI evaluation.
- The Qwen model runs locally and is not stored in the GitHub repository.
- Resume files are stored locally by the application.
- API URLs may differ depending on the local development environment.
- Sensitive credentials and connection strings should not be committed to GitHub.

---

## Related Frontend

The HireSmart frontend is maintained in a separate GitHub repository because the frontend and backend are separate projects.

The frontend is developed using React, Vite, Axios, and React Router.

Frontend Repository:

```text
https://github.com/Ahsan-246/hiresmart-frontend
```

---

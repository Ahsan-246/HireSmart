# HireSmart API

HireSmart API is the backend of the HireSmart recruitment and job management system. It is developed using ASP.NET Core Web API and provides authentication, role-based access, job management, company management, applications, resume handling, dashboards, and AI-based candidate evaluation.

The frontend is maintained in a separate React repository.

---

## Technologies

- ASP.NET Core Web API
- .NET 10
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- PdfPig
- Ollama
- Qwen 2.5:3B

---

## User Roles

The API supports three main roles:

- Candidate
- Recruiter
- Admin

Role-based authorization is applied to protected endpoints.

---

## Main Features

### Authentication

- Candidate registration
- User login
- JWT-based authentication
- Role-based authorization

### Candidate

- Candidate dashboard
- Browse jobs
- Search jobs
- View job details
- Apply for jobs
- Upload resume
- Download/view resume
- Delete resume
- View submitted applications
- Delete applications
- Track application status

### Recruiter

- Recruiter dashboard
- Create jobs
- Edit jobs
- Delete jobs
- View applications for jobs
- View candidate information
- View candidate resumes
- Update application status

### Administrator

- Admin dashboard
- Manage users
- Manage jobs
- Manage companies
- Edit companies
- Delete system records where authorized

### AI Candidate Evaluation

The API includes an AI-based candidate evaluation module.

The evaluation process:

1. Retrieves the selected application.
2. Retrieves the associated job and candidate resume.
3. Extracts text from the candidate's PDF resume using PdfPig.
4. Sends the job title, job description, and extracted resume text to the local AI model.
5. Uses Qwen 2.5:3B through Ollama.
6. Generates:
   - Match Score
   - Missing Skills
   - Candidate Summary
   - Recommendation
7. Stores the evaluation in the database.

The AI is instructed to evaluate candidates based on requirements explicitly stated in the job description rather than inventing additional requirements.

---

## AI Evaluation

AI evaluation uses:

- Ollama
- Qwen 2.5:3B
- PdfPig

Ollama runs locally and the AI model is not included in the repository.

The required model is:

```text
qwen2.5:3b
```

---

## API Modules

The backend contains the following main API controllers:

- Authentication
- Users
- Companies
- Jobs
- Applications
- Resumes
- AI Evaluation
- Candidate Dashboard
- Recruiter Dashboard
- Admin Dashboard

---

## API Operations

### Authentication

- Register
- Login

### Jobs

- Create job
- Get all jobs
- Get job by ID
- Update job
- Delete job
- Search jobs

### Applications

- Create application
- Get applications
- Get application by ID
- Get current user's applications
- Get applications for a job
- Update application
- Update application status
- Delete application
- Download application resume

### Resumes

- Upload resume
- Get resumes
- Get resume by ID
- Download resume
- Delete resume

### Companies

- Create company
- Get companies
- Get company by ID
- Update company
- Delete company

### Users

- Get users
- Get user by ID
- Update user
- Delete user

### AI Evaluation

- Evaluate application
- Get all evaluations
- Get evaluation by ID
- Delete evaluation

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
│   └── Implementations
│
├── Migrations
│
├── Uploads
│
├── Program.cs
├── appsettings.json
└── HireSmart.API.csproj
```

---

## Database

The application uses SQL Server with Entity Framework Core.

The database contains data related to:

- Users
- Companies
- Jobs
- Applications
- Resumes
- AI Evaluations

Entity Framework Core migrations are included in the project.

---

## Configuration

The API requires a SQL Server connection string and JWT configuration.

These values are stored in the application's configuration files.

Sensitive credentials and secret keys should be configured locally and should not be committed to a public repository.

---

## Running the API

The API can be run using Visual Studio or the .NET CLI.

After configuring SQL Server and the required local services, the application can be started with:

```bash
dotnet run
```

Swagger/OpenAPI can then be used to explore and test the available endpoints.

---

## Ollama Configuration

For AI evaluation, Ollama must be installed and running locally.

The required model can be downloaded using:

```bash
ollama pull qwen2.5:3b
```

The API communicates with Ollama through its local HTTP API.

---

## Frontend

The HireSmart React frontend is maintained in a separate repository.

The frontend communicates with this API using Axios.

Frontend repository:

```text
<https://github.com/Ahsan-246/hiresmart-frontend>
```

---

## Testing

The API can be tested using:

- Swagger
- Postman

The API supports testing of authentication, CRUD operations, applications, resume handling, job management, and AI evaluation.

---

## Notes

- SQL Server is required for database functionality.
- Ollama and Qwen 2.5:3B are required for AI evaluation.
- Resume AI evaluation currently processes PDF resumes.
- The frontend and backend are maintained in separate repositories.
- Local configuration values and secret keys should not be committed publicly.

---

## Author

Developed as a full-stack recruitment management project using ASP.NET Core Web API and React.
# HireSmart

HireSmart is a full-stack job recruitment and management platform developed using ASP.NET Core Web API and React.

The system provides separate functionality for Candidates, Recruiters, and Administrators, including job management, applications, resume handling, and AI-based candidate evaluation.

---

## Technologies Used

### Backend
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- CsvHelper
- ClosedXML
- PdfPig
- Ollama
- Qwen 2.5:3B

### Frontend
- React
- JavaScript
- React Router
- Axios
- Vite
- CSS

### Development Tools
- Visual Studio
- Visual Studio Code
- SQL Server
- Git
- GitHub
- Postman
- Swagger

---

## Main Features

### Candidate

- Register and login
- Browse available jobs
- Search jobs
- View job details
- Apply for jobs
- Upload resume
- View uploaded resume
- Delete resume
- View submitted applications
- Delete applications
- Track application status

### Recruiter

- Login
- Create jobs
- Edit jobs
- Delete jobs
- View candidate applications
- View candidate resumes
- Update application status
- Manage recruitment activities

### Administrator

- Manage users
- Manage jobs
- Manage companies
- Edit companies
- View and manage system data

### AI Candidate Evaluation

HireSmart includes an AI-based candidate evaluation module.

The system:

1. Retrieves the candidate's application.
2. Reads the candidate's uploaded PDF resume.
3. Retrieves the job title, description, and required skills.
4. Sends the job requirements and resume information to the local AI model.
5. Uses Qwen 2.5:3B through Ollama.
6. Generates:
   - Match Score
   - Missing Skills
   - Candidate Summary
   - Recommendation
7. Stores the evaluation in the database.

The evaluation is primarily based on the skills explicitly defined for the job.

---

## AI Evaluation

AI evaluation uses:

- Ollama
- Qwen 2.5:3B
- PdfPig for PDF text extraction

### Ollama Setup

Install Ollama and download the required model:

```bash
ollama pull qwen2.5:3b

Make sure Ollama is running before using the AI evaluation feature.

The AI model runs locally and is not included inside the Git repository.

Backend Setup
1. Clone the Repository
git clone <YOUR-GITHUB-REPOSITORY-URL>
2. Open the Backend Project

Open the ASP.NET Core API project in Visual Studio.

3. Configure SQL Server

Update the connection string in:

appsettings.json

according to your local SQL Server configuration.

Example:

"ConnectionStrings": {
  "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
}
4. Apply Database Migrations

Run:

dotnet ef database update
5. Run the API

Run the project from Visual Studio or use:

dotnet run

Swagger will be available at the configured API URL.

Frontend Setup

Navigate to the frontend directory:

cd hiresmart-frontend

Install dependencies:

npm install

Run the development server:

npm run dev

The frontend will be available at the Vite development URL shown in the terminal.

API Modules

The backend provides APIs for:

Authentication
Users
Companies
Jobs
Applications
Resumes
AI Evaluations
CSV import/export
Excel export
File Handling

HireSmart supports:

Resume upload
Resume viewing
Resume deletion
CSV product import
CSV product export
Excel product export

Uploaded resume files are stored locally according to the application's configured file storage.

Authentication

The API uses JWT authentication and role-based authorization.

Available roles include:

Candidate
Recruiter
Admin

Protected endpoints require a valid authentication token and appropriate role.

Testing

API endpoints can be tested using:

Swagger
Postman

The application includes functionality for testing:

Authentication
CRUD operations
Job management
Applications
Resume handling
AI evaluation
File import/export
Project Structure
Backend
HireSmart.API
│
├── Controllers
├── Data
├── DTOs
├── Models
├── Services
│   ├── Interfaces
│   └── Implementation
├── Migrations
└── Program.cs
Frontend
hiresmart-frontend
│
├── src
│   ├── components
│   ├── context
│   ├── pages
│   ├── services
│   └── App.jsx
│
├── public
└── package.json
Important Notes
SQL Server must be available locally.
The database connection string must be configured before running the API.
Ollama must be installed for AI evaluation.
The qwen2.5:3b model must be downloaded before using AI evaluation.
API and frontend URLs may differ depending on the local development environment.
Sensitive credentials and connection strings should not be committed to GitHub.
Author

Developed as a full-stack web application project using ASP.NET Core Web API and React.
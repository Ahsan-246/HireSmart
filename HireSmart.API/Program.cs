using HireSmart.API.Data;
using HireSmart.API.Services;
using HireSmart.API.Services.Implementations;
using HireSmart.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using HireSmart.API.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddScoped<IResumeService, ResumeService>();

builder.Services.AddScoped<IAIEvaluationService,  AIEvaluationService>();

builder.Services.AddScoped<IRecruiterDashboardService, RecruiterDashboardService>();

builder.Services.AddScoped<ICandidateDashboardService, CandidateDashboardService>();

builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();

builder.Services.AddHttpClient<IOllamaService, OllamaService>(
    client =>
    {
        client.BaseAddress =
            new Uri("http://localhost:11434/");
    });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

var app = builder.Build();

app.UseCors("ReactFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

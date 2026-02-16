// ------------------------------------------------------------
// Program.cs - .NET 8 Web API (Local + Docker + ECS Ready)
// ------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// 1️⃣ Configure Kestrel (Required for Docker / ECS)
// ------------------------------------------------------------
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(2535);
});

// ------------------------------------------------------------
// 2️⃣ Services Registration
// ------------------------------------------------------------

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// CORS Policy (LOCAL + CLOUD SUPPORT)
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCalculator", policy =>
    {
        policy
            .WithOrigins(
                // 🌍 Production Domains
                "https://itops.fun",
                "https://www.itops.fun",

                // 🖥 Local Browser Testing
                "http://localhost:9595",
                "http://localhost:2535",

                // 🐳 Docker Desktop (Windows/Mac)
                "http://host.docker.internal:9595"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// ------------------------------------------------------------
// 3️⃣ Middleware Pipeline
// ------------------------------------------------------------

// Swagger (Available everywhere for now)
app.UseSwagger();
app.UseSwaggerUI();

// 🔥 CORS MUST COME BEFORE AUTHORIZATION
app.UseCors("AllowCalculator");

app.UseAuthorization();

// Map Controllers
app.MapControllers();

// ------------------------------------------------------------
// 4️⃣ Health Check Endpoint
// ------------------------------------------------------------
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        version = "2.0",
        environment = app.Environment.EnvironmentName,
        serverTimeUtc = DateTime.UtcNow
    });
});

// ------------------------------------------------------------
// 5️⃣ Start Application
// ------------------------------------------------------------
app.Run();

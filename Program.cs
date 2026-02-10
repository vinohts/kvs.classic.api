// ------------------------------------------------------------
// Program.cs
// Entry point of the .NET 8 Web API application
// ------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ------------------------------------------------------------
// 1️⃣ Create the application builder
// ------------------------------------------------------------
// This initializes:
// - Dependency Injection (DI)
// - Configuration (appsettings.json, env vars)
// - Logging
// - Kestrel web server
var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// 2️⃣ Configure Kestrel (CRITICAL FOR DOCKER / ECS)
// ------------------------------------------------------------
// Listen on port 2535 on ALL network interfaces (0.0.0.0)
//
// Why?
// - Docker containers do NOT use localhost
// - ECS + ALB need the app exposed externally
// - Matches Dockerfile EXPOSE 2535
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(2535);
});

// ------------------------------------------------------------
// 3️⃣ Register services (Dependency Injection)
// ------------------------------------------------------------

// Registers MVC controllers (enables [ApiController])
builder.Services.AddControllers();

// Enables Swagger endpoint discovery
builder.Services.AddEndpointsApiExplorer();

// Generates Swagger/OpenAPI documentation
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// 4️⃣ Build the application
// ------------------------------------------------------------
// Finalizes the service container and middleware pipeline
var app = builder.Build();

// ------------------------------------------------------------
// 5️⃣ Configure middleware (HTTP pipeline)
// ------------------------------------------------------------

// Enables Swagger JSON endpoint (/swagger/v1/swagger.json)
app.UseSwagger();

// Enables Swagger UI (/swagger)
app.UseSwaggerUI();

// Enables authorization middleware
// (even if no auth is configured yet)
app.UseAuthorization();

// ------------------------------------------------------------
// 6️⃣ Configure routing
// ------------------------------------------------------------

// Maps controller routes like:
// GET /api/workflows
// POST /api/workflows
app.MapControllers();

// ------------------------------------------------------------
// 7️⃣ Start the application
// ------------------------------------------------------------
// Starts Kestrel and begins listening on port 2535
app.Run();

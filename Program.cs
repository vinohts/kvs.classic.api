// Create the application builder
// This sets up dependency injection, configuration, logging, etc.
var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES REGISTRATION --------------------

// Register MVC controllers so routes like /api/workflows can work
builder.Services.AddControllers();

// Required for Swagger endpoint discovery
builder.Services.AddEndpointsApiExplorer();

// Registers Swagger generator
builder.Services.AddSwaggerGen();

// -------------------- BUILD APPLICATION --------------------

// Build the application pipeline
var app = builder.Build();

// -------------------- MIDDLEWARE CONFIGURATION --------------------

// Enable Swagger JSON endpoint
app.UseSwagger();

// Enable Swagger UI (browser-based API testing)
app.UseSwaggerUI();

// Enable authorization middleware (even if not used yet)
// This is common in enterprise APIs
app.UseAuthorization();

// -------------------- ROUTING --------------------

// Map all controller routes (CRITICAL)
// Without this, /api/workflows will return 404
app.MapControllers();

// -------------------- START APPLICATION --------------------

// Start the web server (Kestrel)
app.Run();

using BridgeAPI.Services;
using System.Diagnostics;

// BridgeAPI - REST API para conectar JavaScript a SQL Server/SQLite
// Versao: 1.0.2 - Producao (Enhanced: error logging for Render diagnostics)

Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] 🚀 BridgeAPI starting...");
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}");
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] PORT: {Environment.GetEnvironmentVariable("PORT") ?? "5000"}");

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ WebApplication.CreateBuilder initialized");

// Configure URLs - Render uses PORT environment variable
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ WebHost configured to listen on 0.0.0.0:{port}");

// Add services to the container
builder.Services.AddControllers();
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ Controllers added");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ Swagger added");

// Register custom services
try
{
    builder.Services.AddScoped<IDatabaseService, DatabaseService>();
    builder.Services.AddScoped<IConnectionManager, ConnectionManager>();
    Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ Database services registered");
}
catch (Exception ex)
{
    Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ❌ ERROR registering database services: {ex.Message}");
    throw;
}

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ CORS configured");

var app = builder.Build();
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ WebApplication built");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ℹ️  Running in Development mode - Swagger enabled");
}
else
{
    Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ℹ️  Running in Production mode");
}

// REMOVED: app.UseHttpsRedirection() - Render handles TLS at proxy level
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ Middleware configured - ready to start");
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ✅ All systems ready - calling app.Run()");
Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] 💥 FATAL ERROR during app.Run(): {ex.Message}");
    Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Stack: {ex.StackTrace}");
    throw;
}

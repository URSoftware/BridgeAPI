using BridgeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddLogging();

// Disable HTTPS redirect (Render handles TLS at proxy level)
builder.Services.AddHttpsRedirection(options => options.HttpsPort = null);

try
{
    builder.Services.AddScoped<IDatabaseService, DatabaseService>();
    builder.Services.AddScoped<IConnectionManager, ConnectionManager>();
}
catch { }

var app = builder.Build();

app.MapControllers();
app.Run();

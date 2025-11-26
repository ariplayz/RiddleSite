using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (minimal for Kestrel-backed API)

// Allow browser client to call the API during development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Explicitly configure Kestrel (enabled by default, but we make it explicit per requirement)
builder.WebHost.UseKestrel();

var app = builder.Build();

app.UseCors();

// No Swagger to keep dependencies minimal. Add if needed later.

// Simple endpoints
app.MapGet("/", () => Results.Ok(new { name = "RiddleSite.Web", status = "ok" }));
app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();

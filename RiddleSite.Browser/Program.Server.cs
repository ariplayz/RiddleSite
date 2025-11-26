using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

// Minimal Kestrel host that serves the published WASM from wwwroot
var builder = WebApplication.CreateBuilder(args);

// Explicitly use Kestrel
builder.WebHost.UseKestrel();

var app = builder.Build();

// Serve index.html by default and static files from ./wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Simple health endpoint
app.MapGet("/health", () => "Healthy");

app.Run();

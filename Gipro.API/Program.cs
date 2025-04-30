using Gipro.Application.Services;
using Gipro.Infrastructure.Servicios;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Agregar servicios
// -------------------------

builder.Services.AddControllers();

// Inyecci�n de dependencias: IUsuarioService
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gipro API",
        Version = "v1",
        Description = "API para la gesti�n de proveedores en Gipro"
    });
});

// CORS (para consumir desde Razor Pages o Postman sin problemas)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// -------------------------
// Middleware
// -------------------------

// Swagger UI en la ra�z
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gipro API v1");
    c.RoutePrefix = string.Empty; // Esto hace que Swagger est� en la ra�z "/"
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

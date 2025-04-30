using Gipro.Application.DTOs;
using Gipro.Application.Services;

namespace Gipro.Infrastructure.Servicios;

public class UsuarioService : IUsuarioService
{
    public Task RegistrarUsuarioAsync(UsuarioDTO usuario)
    {
        // Acá deberías guardar en base de datos (mock por ahora)
        Console.WriteLine($"Registrando usuario: {usuario.Nombre} - {usuario.Apellido}");
        return Task.CompletedTask;
    }
}

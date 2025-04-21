using Gipro.Application.DTOs;
using Gipro.Application.Services;

namespace Gipro.Infrastructure;

public class UsuarioService : IUsuarioService
{
    public Task RegistrarUsuarioAsync(UsuarioDto usuario)
    {
        // Acá deberías guardar en base de datos (mock por ahora)
        Console.WriteLine($"Registrando usuario: {usuario.Nombre} - {usuario.Email}");
        return Task.CompletedTask;
    }
}

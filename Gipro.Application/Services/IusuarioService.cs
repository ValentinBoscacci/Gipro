using Gipro.Application.DTOs;

namespace Gipro.Application.Services;

public interface IUsuarioService
{
    Task RegistrarUsuarioAsync(UsuarioDTO usuario);
}

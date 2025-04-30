using Gipro.Application.DTOs;
using Gipro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gipro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDTO usuario)
        {
            await _usuarioService.RegistrarUsuarioAsync(usuario);
            return Ok(new { mensaje = "Usuario registrado con éxito" });
        }
    }
}



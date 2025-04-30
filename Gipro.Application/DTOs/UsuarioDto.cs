using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gipro.Application.DTOs
{
    public class UsuarioDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Rol { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public string ImagenPerfil { get; set; }
        public string Token { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public string UltimaIP { get; set; }
        public string UltimoDispositivo { get; set; }
        public string UltimaUbicacion { get; set; }
        public string UltimaVersion { get; set; }
        public string UltimoSistemaOperativo { get; set; }

        public string UltimoNavegador { get; set; }
        public string UltimoIdioma { get; set; }

    }
}

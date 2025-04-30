using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Core.dominio
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }  // Ej: "Admin", "Auditor"

        public string Permisos { get; set; }  // JSON: { "puedeEditar": true, ... }

        // Relación muchos a muchos con Usuario
        public ICollection<UsuarioRol> Usuarios { get; set; } = new List<UsuarioRol>();

        // Relación 1 a muchos con Menu
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
    }
}

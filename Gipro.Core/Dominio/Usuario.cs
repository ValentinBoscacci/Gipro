using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        public string? AzureId { get; set; }  // Nullable para Azure AD B2C

        // Relación muchos a muchos con Rol
        public ICollection<UsuarioRol> Roles { get; set; } = new List<UsuarioRol>();
    }
}

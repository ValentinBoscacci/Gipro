using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.dominio
{
    public class Contratista
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(13)]
        public string CUIT { get; set; }

        [StringLength(200)]
        public string Domicilio { get; set; }

        [StringLength(100)]
        public string Localidad { get; set; }

        public string SituacionAfip { get; set; }  // Enum recomendado
        public string Actividad { get; set; }

        // Relación muchos a 1 con Empresa
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        // Relación 1 a muchos con Contactos
        public ICollection<ContratistaContacto> Contactos { get; set; } = new List<ContratistaContacto>();

        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
        public DateTime? FechaBaja { get; set; }  // Nullable
        public bool Activo { get; set; } = true;
    }
}


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContratistaId { get; set; }

        [ForeignKey("ContratistaId")]
        public Contratista Contratista { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20)]
        public string DNI { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        public DateTime? FechaBaja { get; set; }  // Nullable
    }
}

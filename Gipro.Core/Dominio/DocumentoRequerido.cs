using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.dominio  
{
    public class DocumentoRequerido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }  // Ej: "ART", "F931"

        [Required]
        public bool Obligatorio { get; set; }

        [Required]
        public string AplicableA { get; set; }  // Enum recomendado: "Contratista", "Empleado", "AvisoDeTrabajo"

        public string? Condicion { get; set; }  // Ej: "Riesgo = alto" (nullable)
    }
}
using System.ComponentModel.DataAnnotations;

namespace Core.dominio
{
    public class AspectoEvaluado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }  // Ej: "Cumplimiento en tiempo y forma"

        public string? Descripcion { get; set; }
    }
}

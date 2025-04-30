using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio

{
    public class Evaluacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContratistaId { get; set; }

        [ForeignKey("ContratistaId")]
        public Contratista Contratista { get; set; }

        [Required]
        public int UsuarioId { get; set; }  // FK a Usuario

        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [Required]
        public string Sector { get; set; }  // Enum: "Contable", "Higiene", etc.

        // Relación con resultados
        public ICollection<ResultadoEvaluacion> Resultados { get; set; } = new List<ResultadoEvaluacion>();
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio

{
    public class ResultadoEvaluacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EvaluacionId { get; set; }

        [ForeignKey("EvaluacionId")]
        public Evaluacion Evaluacion { get; set; }

        [Required]
        public int AspectoEvaluadoId { get; set; }

        [ForeignKey("AspectoEvaluadoId")]
        public AspectoEvaluado AspectoEvaluado { get; set; }

        [Range(1, 5)]
        public int Puntaje { get; set; }

        public string? Observaciones { get; set; }
    }
}

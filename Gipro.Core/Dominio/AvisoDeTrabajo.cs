using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio
{
    public class AvisoDeTrabajo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContratistaId { get; set; }

        [ForeignKey("ContratistaId")]
        public Contratista Contratista { get; set; }

        [Required]
        public DateTime FechaDesde { get; set; }

        [Required]
        public DateTime FechaHasta { get; set; }

        public int EstablecimientoId { get; set; }  // FK a tabla Establecimiento (no definida aún)
        public string Sector { get; set; }
        public string Tarea { get; set; }

        [Range(1, 1000)]
        public int CantidadPersonas { get; set; }

        public string Horarios { get; set; }  // Ej: "L-V 8:00-17:00"
        public bool IngresanEmpleados { get; set; }

        [Required]
        public string Riesgo { get; set; }  // Enum recomendado: "Bajo", "Medio", "Alto"

        // Relación con documentos
        public ICollection<DocumentoPresentado> Documentos { get; set; } = new List<DocumentoPresentado>();
    }
}

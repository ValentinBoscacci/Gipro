
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Core.dominio
{
    public class DocumentoPresentado
    {
        [Key]
        public int Id { get; set; }

        public int? ContratistaId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? AvisoDeTrabajoId { get; set; }

        [Required]
        public int DocumentoRequeridoId { get; set; }

        [ForeignKey("DocumentoRequeridoId")]
        public DocumentoRequerido DocumentoRequerido { get; set; }

        [Required]
        public DateTime FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }  // Nullable

        [Required]
        public string ArchivoUrl { get; set; }  // Ruta en Azure Blob

        public string? Observaciones { get; set; }

        // Relaciones condicionales
        [ForeignKey("ContratistaId")]
        public Contratista? Contratista { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }

        [ForeignKey("AvisoDeTrabajoId")]
        public AvisoDeTrabajo? AvisoDeTrabajo { get; set; }
    }
}

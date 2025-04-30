
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContratistaId { get; set; }

        [ForeignKey("ContratistaId")]
        public Contratista Contratista { get; set; }

        [Required]
        [StringLength(10)]
        public string Dominio { get; set; }  // Patente

        [Required]
        public string Tipo { get; set; }  // Enum: "Camion", "Auto", "Moto"

        public DateTime? SeguroVigenteHasta { get; set; }  // Nullable
    }
}

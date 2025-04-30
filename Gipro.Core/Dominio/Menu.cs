using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.dominio

{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        public int? IdPadre { get; set; }  // Nullable para items raíz

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        public string Ruta { get; set; }  // Ej: "/contratistas"

        public int Orden { get; set; }

        [Required]
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
    }
}

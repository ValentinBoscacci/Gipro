using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.dominio
{
        public class ContratistaContacto
        {
            public int Id { get; set; }
            public int ContratistaId { get; set; }

            public TipoContacto Tipo { get; set; }  // Enum

            [StringLength(100)]
            public string Valor { get; set; }

            public bool EsPrincipal { get; set; } = false;

            // Relación muchos a 1 con Contratista
            [ForeignKey("ContratistaId")]
            public Contratista Contratista { get; set; }
        }

        public enum TipoContacto
        {
            Telefono,
            Email,
            RedSocial,
            Otro
        }
    }

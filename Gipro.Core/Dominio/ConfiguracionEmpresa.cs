using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.dominio
{
    public class ConfiguracionEmpresa
    {
        public int Id { get; set; }
        public string FormatoFactura { get; set; }
        public string MonedaPrincipal { get; set; }

        // Relación 1 a 1 con Empresa
        public Empresa Empresa { get; set; }
    }
}

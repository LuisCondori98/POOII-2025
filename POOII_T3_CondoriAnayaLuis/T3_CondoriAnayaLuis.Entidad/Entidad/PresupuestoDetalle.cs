using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3_CondoriAnayaLuis.Entidad.Entidad
{
    public class PresupuestoDetalle
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal SubTotal { get; set; }
    }
}

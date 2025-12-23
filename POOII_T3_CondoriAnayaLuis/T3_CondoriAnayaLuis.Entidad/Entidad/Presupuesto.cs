using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace T3_CondoriAnayaLuis.Entidad.Entidad
{
    public class Presupuesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public IList<PresupuestoDetalle> presupuestoDetalle { get; set; }

        public Presupuesto()
        {
            presupuestoDetalle = new List<PresupuestoDetalle>();
        }
    }
}

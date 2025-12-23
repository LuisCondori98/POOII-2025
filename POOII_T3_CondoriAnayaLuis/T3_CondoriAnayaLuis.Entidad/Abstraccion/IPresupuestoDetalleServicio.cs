using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T3_CondoriAnayaLuis.Entidad.Entidad;

namespace T3_CondoriAnayaLuis.Entidad.Abstraccion
{
    public interface IPresupuestoDetalleServicio
    {
        IEnumerable<PresupuestoDetalle> presupuestoDetalles(int id);
    }
}

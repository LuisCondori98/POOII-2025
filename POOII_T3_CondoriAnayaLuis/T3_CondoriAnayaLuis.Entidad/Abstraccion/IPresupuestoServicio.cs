using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T3_CondoriAnayaLuis.Entidad.Entidad;

namespace T3_CondoriAnayaLuis.Entidad.Abstraccion
{
    public interface IPresupuestoServicio
    {
        IEnumerable<Presupuesto> filtrarPresupuestos(string filtro = "");
        Presupuesto obtenerPresupuesto(int id);
        string insertar_Presupuesto(Presupuesto presupuesto);
        string eliminar_Presupuesto(int id);
    }
}

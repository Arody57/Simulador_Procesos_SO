using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_Procesos.InformacionProcesso
{
    public class info_process_grid
    {
        int idItem;
        string nombre_proceso;
        float memoProceso;
        int procesoCpu;
        int tiempoProceso;
        public info_process_grid(int id, string nombre, float memo, int procesCpu, int time)
        {
            this.idItem = id;
            this.nombre_proceso = nombre;
            this.memoProceso = memo;
            this.procesoCpu = procesCpu;
            this.tiempoProceso = time;
        }
        public info_process_grid()
        {

        }
        public override string ToString()
        {
            return idItem+","+nombre_proceso + "," + memoProceso + "," + tiempoProceso + "," + procesoCpu;
        }
    }

}

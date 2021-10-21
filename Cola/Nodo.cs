using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_Procesos.Cola
{
    public class Nodo
    {
        public Object Dato;
        public Nodo Siguiente;
        public Nodo Anterior;

        public Nodo()
        {
            Siguiente = Anterior = null;
        }

        public Nodo(Object pDato)
        {
            Dato = pDato;
            Siguiente = Anterior = null;
        }
    }
}

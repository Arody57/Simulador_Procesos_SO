using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_Procesos.Cola
{
    public class Cola
    {
        public Nodo Primero;
        public Nodo Ultimo;
        public int NumeroElementos;

        public Cola()
        {
            Primero = Ultimo = null;
            NumeroElementos = 0;
        }

        public void Push(Object pDato)
        {
            Nodo NuevoNodo = new Nodo(pDato);
            if (ColaVacia())
            {
                Primero = Ultimo = NuevoNodo;
                NumeroElementos++;
            }
            else
            {
                NuevoNodo.Anterior = Ultimo;
                Ultimo.Siguiente = NuevoNodo;
                Ultimo = NuevoNodo;
                NumeroElementos++;
            }
        }

        public object Pop()
        {
            if (!ColaVacia())
            {
                Object a;
                a = Primero.Dato;
                if (NumeroElementos == 1)
                {
                    BorrarCola();
                    NumeroElementos--;
                }
                else
                {
                    Primero = Primero.Siguiente;
                    NumeroElementos--;
                }
                return a;
            }
            return null;
        }

        public Boolean ColaVacia()
        {
            return (Primero == null);
        }

        public void BorrarCola()
        {
            Primero = null;
            Ultimo = null;
        }
    }
}

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
        public object quitar()
        {
            object aux;
            try
            {
                if (!ColaVacia())
                {
                    aux = Primero.Dato;
                    Primero = Primero.Siguiente;
                }
                else
                {
                    throw new Exception("No se puede eliminar de una cola vacia");
                }
            }
            catch (Exception)
            {
                aux = null;
            }
            return aux;
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
        //Obtener el primer elemento de la cola
        public object frenteCola()
        {
            if (ColaVacia())
            {
                throw new Exception("Pila vacia, no se puede leer ningun elemento");
            }

            return Primero.Dato;
        }
    }
}

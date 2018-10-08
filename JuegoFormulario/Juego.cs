using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoFormulario
{
    class Juego
    {
        List<int> listaDeUsuario; //Almacena los botones que toca el usuario
        List<int> listaNumeroUsados; //Almacena los botones usados
        List<List<int>> listaDeSecuencia; //Almacena la lista de las secuencias
        int niveles;

        public int Niveles { get => niveles; set => niveles = value; }
        public List<int> ListaDeUsuario { get => listaDeUsuario; set => listaDeUsuario = value; }
        public List<int> ListaNumeroUsados { get => listaNumeroUsados; set => listaNumeroUsados = value; }
        public List<List<int>> ListaDeSecuencia { get => listaDeSecuencia; set => listaDeSecuencia = value; }

        //Constructor para instaciar los objetos
        public Juego()
        {
            listaDeSecuencia = new List<List<int>>();
            listaDeUsuario = new List<int>();
            listaNumeroUsados = new List<int>();
            niveles = 0;
        }

        //Compara la ultima secuencia de la lista de secuencias con la del usuario
        public bool CompararSecuencias()
        {
            int cont = 0;
            foreach(int item in listaDeSecuencia[listaDeSecuencia.Count() - 1])
            {
                if(item != ListaDeUsuario[cont])
                {
                    return false;
                }
                cont++;
            }
            return true;
        }

        //Me arma la secuencia con una lista de listas de acuerdo al nivel (maximo 6 intentos)
        public void ArmarSecuencia()
        {
            List<int> nuevaLista = new List<int>();
            NumeroAleatorio();
            foreach(int item in listaNumeroUsados)
            {
                nuevaLista.Add(item);
            }
            listaDeSecuencia.Add(nuevaLista);
            niveles++;
        }

        //Me arma la lista de usuario de acuerdo al boton que toque el usuario
        public void SecuenciaUsuario(int num)
        {
            listaDeUsuario.Add(num);
        }

        public void LimpiarSecuencias()
        {
            listaDeUsuario.Clear();
        }

        //Muestra las secuencias
        public string MostrarListaSecuencias()
        {
            string cadena = "";
            foreach(List<int> item in listaDeSecuencia)
            {
                cadena += "[";
                foreach(int entero in item)
                {
                    cadena += entero + ",";
                }
                cadena += "] ; ";
            }
            return cadena;
        }

        //Muestra la secuencia del usuario
        public string MostrarListaUsuario()
        {
            string cadena = "[";
            foreach (int item in listaDeUsuario)
            {
                cadena += item + ",";
            }
            cadena += "]";
            return cadena;
        }

        //Devuelve un numero aleatorio para los botones evitando que este se repita
        public int NumeroAleatorio()
        {
            int aux;
            Random numero = new Random();
            do
            {     
                aux = numero.Next(1, 7);
            } while (listaNumeroUsados.Contains(aux));

            listaNumeroUsados.Add(aux);

            return aux;    
        }

        //Devuelvo la cantidad de items de la ultima secuencia
        public int CantSec()
        {
            return ListaDeSecuencia[listaDeSecuencia.Count() - 1].Count();
        }

        //Devuelvo la cantidad de items de la secuencia del usuario
        public int CantUsuar()
        {
            return ListaDeUsuario.Count();
        }



    }
}

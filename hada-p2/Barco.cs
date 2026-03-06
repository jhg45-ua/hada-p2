using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    { //aqui esta el codigo, estte es mi trabajo, os dejo el resto
        public Dictionary<Coordenada, String> CoordenadasBarco;
        public string Nombre;
        public int NumDanyos;

        public EventHandler<TocadoArgs> eventoTocado;
        public EventHandler<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            Nombre = nombre;

            CoordenadasBarco = new Dictionary<Coordenada, string>();
            CoordenadasBarco.Add(coordenadaInicio, Nombre);

            if (orientacion == 'h')
            {
                for (int i = 1; i < longitud; i++)
                {
                    CoordenadasBarco.Add(new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i), Nombre);
                }
            }
            else if (orientacion == 'v')
            {
                for (int i = 1; i < longitud; i++)
                {
                    CoordenadasBarco.Add(new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna), Nombre);
                }
            }
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c))
            {
                CoordenadasBarco[c] = c.ToString() + "_T";
                NumDanyos++;
                
                if (eventoTocado != null) // Verificar si hay suscriptores antes de invocar el evento
                {
                    eventoTocado(this, new TocadoArgs(CoordenadasBarco[c], c));
                }

            }
        }

        bool hundido()
        {
            for (int i = 0; i < CoordenadasBarco.Count; i++)
            {
                if (CoordenadasBarco.ElementAt(i).Value == Nombre)
                {
                    return false;
                }
            }

            return true;
        }

        String toString()
        {
            if (hundido())
            {
                String output = "[" + Nombre + "] - Daños :[" + NumDanyos + "] - HUNDIDO: [TRUE] - COORDENADAS: ";

                for (int i = 0; i < CoordenadasBarco.Count; i++)
                {
                    output += "[" + CoordenadasBarco.ElementAt(i).Key.ToString() + " :" + Nombre + "]";
                }

                return output;
            }
            else
            {
                String output = "[" + Nombre + "] - Daños :[" + NumDanyos + "] - HUNDIDO: [FALSE] - COORDENADAS: ";

                for (int i = 0; i < CoordenadasBarco.Count; i++)
                {
                    output += "[" + CoordenadasBarco.ElementAt(i).Key.ToString() + " :" + CoordenadasBarco.ElementAt(i).Value + "]";
                }

                return output;
            }
        }
    }
}

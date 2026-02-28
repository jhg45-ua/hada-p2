using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    { //aqui esta el codigo, estte es mi trabajo, os dejo el resto
        Dictionary<Coordenada, String> CoordenadaBarco;
        string Nombre;
        int NumDanyos;

        public EventHandler<TocadoArgs> eventoTocado;
        public EventHandler<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            Nombre = nombre;

            CoordenadaBarco = new Dictionary<Coordenada, string>();
            CoordenadaBarco.Add(coordenadaInicio, Nombre);

            if (orientacion == 'h')
            {
                for (int i = 1; i < longitud; i++)
                {
                    CoordenadaBarco.Add(new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i), Nombre);
                }
            }
            else if (orientacion == 'v')
            {
                for (int i = 1; i < longitud; i++)
                {
                    CoordenadaBarco.Add(new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna), Nombre);
                }
            }
        }

        void Disparo(Coordenada c)
        {
            if (CoordenadaBarco.ContainsKey(c))
            {
                CoordenadaBarco[c] = c.ToString() + "_T";
                NumDanyos++;
                
                if (eventoTocado != null) // Verificar si hay suscriptores antes de invocar el evento
                {
                    eventoTocado(this, new TocadoArgs(CoordenadaBarco[c], c));
                }

            }
        }

        bool hundido()
        {
            for (int i = 0; i < CoordenadaBarco.Count; i++)
            {
                if (CoordenadaBarco.ElementAt(i).Value == Nombre)
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

                for (int i = 0; i < CoordenadaBarco.Count; i++)
                {
                    output += "[" + CoordenadaBarco.ElementAt(i).Key.ToString() + " :" + Nombre + "]";
                }

                return output;
            }
            else
            {
                String output = "[" + Nombre + "] - Daños :[" + NumDanyos + "] - HUNDIDO: [FALSE] - COORDENADAS: ";

                for (int i = 0; i < CoordenadaBarco.Count; i++)
                {
                    output += "[" + CoordenadaBarco.ElementAt(i).Key.ToString() + " :" + CoordenadaBarco.ElementAt(i).Value + "]";
                }

                return output;
            }
        }
    }
}

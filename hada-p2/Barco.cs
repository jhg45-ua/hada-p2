using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {
        public Dictionary<Coordenada, String> CoordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            if (longitud <= 0)
            {
                throw new ArgumentException("La longitud del barco debe ser mayor que 0.");
            }

            if (orientacion != 'h' && orientacion != 'v')
            {
                throw new ArgumentException("La orientación del barco debe ser 'h' o 'v'.");
            }

            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

            for (int i = 0; i < longitud; i++)
            {
                Coordenada coordenadaActual;

                if (orientacion == 'h')
                {
                    coordenadaActual = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                }
                else
                {
                    coordenadaActual = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                }

                CoordenadasBarco.Add(coordenadaActual, Nombre);
            }
        }

        public void Disparo(Coordenada c)
        {
            if (!CoordenadasBarco.ContainsKey(c))
            {
                return;
            }

            // Si la casilla ya estaba tocada no hay cambios ni eventos.
            if (CoordenadasBarco[c] != Nombre)
            {
                return;
            }

            CoordenadasBarco[c] = Nombre + "_T";
            NumDanyos++;

            eventoTocado?.Invoke(this, new TocadoArgs(Nombre, new Coordenada(c)));

            if (hundido())
            {
                eventoHundido?.Invoke(this, new HundidoArgs(Nombre));
            }
        }

        public bool hundido()
        {
            foreach (string etiqueta in CoordenadasBarco.Values)
            {
                if (etiqueta == Nombre)
                {
                    return false;
                }
            }

            return true;
        }

        public override String ToString()
        {
            String output = "[" + Nombre + "] - Daños :[" + NumDanyos + "] - HUNDIDO: [" + (hundido() ? "TRUE" : "FALSE") + "] - COORDENADAS: ";

            foreach (KeyValuePair<Coordenada, string> coordenada in CoordenadasBarco)
            {
                output += "[" + coordenada.Key.ToString() + " :" + coordenada.Value + "]";
            }

            return output;
        }
    }
}

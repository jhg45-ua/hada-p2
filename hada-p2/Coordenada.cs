using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        // Atributos
        public int Fila;
        public int Columna;

        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }

        public Coordenada(int fila, int columna)
        {
            this.Fila = fila;
            this.Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            this.Fila = int.Parse(fila);
            this.Columna = int.Parse(columna);
        }

        public override string ToString()
        {
            return "(" + this.Fila + "," + this.Columna + ")";
        }

        public Coordenada(Coordenada otra)
        {
            this.Fila = otra.Fila;
            this.Columna = otra.Columna;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordenada coordenada &&
                   Fila == coordenada.Fila &&
                   Columna == coordenada.Columna;
        }

        public override int GetHashCode()
        {
            int hashCode = 1681403097;
            hashCode = hashCode * -1521134295 + Fila.GetHashCode();
            hashCode = hashCode * -1521134295 + Columna.GetHashCode();
            return hashCode;
        }
    }
}

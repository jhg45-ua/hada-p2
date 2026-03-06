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
        private int _fila;
        public int Fila
        {
            get { return _fila; }
            set
            {
                if (value >= 0 && value <= 9) _fila = value;
                else throw new ArgumentException("La fila debe tener un valor entre 0 y 9.");
            }
        }

        private int _columna;
        public int Columna
        {
            get { return _columna; }
            set
            {
                if (value >= 0 && value <= 9) _columna = value;
                else throw new ArgumentException("La columna debe tener un valor entre 0 y 9.");
            }
        }

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

        public bool Equals(Coordenada coordenada)
        {
            if (coordenada == null) return false;
            return this.Fila == coordenada.Fila && this.Columna == coordenada.Columna;
        }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }
    }
}

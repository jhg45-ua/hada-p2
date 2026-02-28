using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        // Atributos
        public int TamTablero 
        { 
            get { return _tamTablero; }
            set
            {
                if (value >= 4 && value <= 9)
                {
                    _tamTablero = value;
                }
                else
                {
                    throw new ArgumentException("El tamaño del tablero debe ser entre 4 y 9.");
                }
            } 
        }
        private int _tamTablero;

        private List<Coordenada> coordenadasDisparadas { get; set; }
        private List<Coordenada> coordenadasTocadas { get; set; }
        private List<Barco> barcos  { get; set; }
        private List<Barco> barcosEliminados    { get; set; }
        private Dictionary<Coordenada, String> casillasTablero  { get; set; }

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            this.TamTablero = tamTablero;
            this.coordenadasDisparadas = new List<Coordenada>();
            this.coordenadasTocadas = new List<Coordenada>();
            this.barcos = barcos;
            this.barcosEliminados = new List<Barco>();
            this.casillasTablero = new Dictionary<Coordenada, string>();

            foreach (Barco barco in barcos)
            {
                barco.eventoTocado += cuandoBarcoTocado;
                barco.eventoHundido += cuandoBarcoHundido;
            }

            for (int fila = 0; fila < tamTablero; fila++)
            {
                for (int columna = 0; columna < tamTablero; columna++)
                {
                    casillasTablero.Add(new Coordenada(fila, columna), "AGUA");
                }
            }
        }

        private void cuandoBarcoTocado(object sender, TocadoArgs e)
        {

        }

        private void cuandoBarcoHundido(object sender, HundidoArgs e)
        {
        }
    }
}

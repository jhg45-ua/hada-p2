using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        public event EventHandler<EventArgs> eventoFinPartida;

        private int _tamTablero;
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

        private List<Coordenada> coordenadasDisparadas { get; set; }
        private List<Coordenada> coordenadasTocadas { get; set; }
        private List<Barco> barcos { get; set; }
        private List<Barco> barcosEliminados { get; set; }
        private Dictionary<Coordenada, String> casillasTablero { get; set; }

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            this.barcos = barcos ?? throw new ArgumentNullException(nameof(barcos));
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            foreach (Barco barco in this.barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

            inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    casillasTablero.Add(new Coordenada(fila, columna), "AGUA");
                }
            }

            foreach (Barco barco in barcos)
            {
                foreach (Coordenada coord in barco.CoordenadasBarco.Keys)
                {
                    casillasTablero[coord] = barco.Nombre;
                }
            }
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            Console.WriteLine($"TABLERO: Barco [{e.nombre}] tocado en Coordenada: [{e.coordenadaImpacto}]");

            casillasTablero[e.coordenadaImpacto] = e.nombre + "_T";

            if (!coordenadasTocadas.Contains(e.coordenadaImpacto))
            {
                coordenadasTocadas.Add(e.coordenadaImpacto);
            }
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine($"TABLERO: Barco [{e.nombre}] hundido!!");

            Barco barcoHundido = barcos.FirstOrDefault(b => b.Nombre == e.nombre);
            if (barcoHundido != null && !barcosEliminados.Contains(barcoHundido))
            {
                barcosEliminados.Add(barcoHundido);
            }

            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Disparar(Coordenada c)
        {
            if (c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine($"La coordenada ({c.Fila},{c.Columna}) está fuera de las dimensiones del tablero.");
                return;
            }

            coordenadasDisparadas.Add(c);

            foreach (Barco barco in barcos)
            {
                barco.Disparo(c);
            }
        }

        public string DibujarTablero()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CASILLAS TABLERO");
            sb.AppendLine("--------");

            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    Coordenada actual = new Coordenada(fila, columna);
                    sb.Append($"[{casillasTablero[actual]}]");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Barco barco in barcos)
            {
                sb.AppendLine(barco.ToString());
            }

            sb.Append("Coordenadas Disparadas: ");
            foreach (Coordenada c in coordenadasDisparadas)
            {
                sb.Append(c.ToString() + " ");
            }
            sb.AppendLine();

            sb.Append("Coordenadas Tocadas: ");
            foreach (Coordenada c in coordenadasTocadas)
            {
                sb.Append(c.ToString() + " ");
            }
            sb.AppendLine();
            sb.AppendLine();

            sb.Append(DibujarTablero());

            return sb.ToString();
        }
    }
}

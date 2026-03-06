using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Game
    {
        private bool finPartida;

        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        private void gameLoop()
        {
            // Barcos de ejemplo: no se solapan y tienen nombres únicos.
            List<Barco> barcos = new List<Barco>()
            {
                new Barco("THOR", 1, 'h', new Coordenada(0, 0)),
                new Barco("MAYA", 3, 'h', new Coordenada(3, 1)),
                new Barco("ZEUS", 2, 'v', new Coordenada(6, 5))
            };

            validarNombresUnicos(barcos);

            Tablero tablero = new Tablero(9, barcos);
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida)
            {
                Console.WriteLine(tablero.ToString());
                Console.WriteLine("Introduce las coordenadas de tu disparo (fila,columna) o 's' para salir:");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Entrada no válida. El formato correcto es NUMERO,NUMERO.");
                    continue;
                }

                input = input.Trim();

                if (string.Equals(input, "s", StringComparison.OrdinalIgnoreCase))
                {
                    finPartida = true;
                    Console.WriteLine("Partida finalizada por el usuario.");
                    continue;
                }

                string[] partes = input.Split(',');
                if (partes.Length != 2)
                {
                    Console.WriteLine("Entrada no válida. El formato correcto es NUMERO,NUMERO.");
                    continue;
                }

                if (!int.TryParse(partes[0].Trim(), out int fila) || !int.TryParse(partes[1].Trim(), out int columna))
                {
                    Console.WriteLine("Entrada no válida. El formato correcto es NUMERO,NUMERO.");
                    continue;
                }

                try
                {
                    Coordenada disparo = new Coordenada(fila, columna);
                    tablero.Disparar(disparo);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Entrada no válida. Los valores deben estar entre 0 y 9.");
                }
            }
        }

        private static void validarNombresUnicos(List<Barco> barcos)
        {
            HashSet<string> nombres = new HashSet<string>(StringComparer.Ordinal);

            foreach (Barco barco in barcos)
            {
                if (!nombres.Add(barco.Nombre))
                {
                    throw new InvalidOperationException("Los nombres de los barcos deben ser únicos.");
                }
            }
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
